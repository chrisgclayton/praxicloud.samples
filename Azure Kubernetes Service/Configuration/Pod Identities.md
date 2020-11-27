# Using Pod Identities (Managed Service Identity)

## Configuring the Cluster to Support Pod Identities

In this section the steps required to setup the cluster to support Pod Identities are outlined. While this demonstrates how to do this for RBAC enabled clusters that use a system assigned managed identity steps are similar and can be found in the AKS documentation for other variations.

Before performing these steps clone the following repository [Azure/aad-pod-identity: Assign Azure Active Directory Identities to Kubernetes applications. (github.com)](https://github.com/Azure/aad-pod-identity). 

### The Code

```bash
# git clone https://github.com/Azure/aad-pod-identity.git
# if repository was cloned from windows it may be required to convert the file to unix line endings with dos2unix

resourceGroup={cluster resource group}
clusterName={cluster name}

export SUBSCRIPTION_ID="{subscription id}"
export RESOURCE_GROUP="$resourceGroup"
export CLUSTER_NAME="$clusterName"
export CLUSTER_LOCATION="{location}"

bash ./hack/role-assignment.sh

# Get the id used in the assignments
assignmentId=$(az aks show -g $resourceGroup -n $clusterName --query identityProfile.kubeletidentity.clientId -otsv)
echo "To assign permissues use Id: $assignmentId"

# Install the Pod Identity 
kubectl apply -f https://raw.githubusercontent.com/Azure/aad-pod-identity/v1.7.0/deploy/infra/deployment-rbac.yaml

# If not RBAC run this command instead of the previous
# kubectl apply -f https://raw.githubusercontent.com/Azure/aad-pod-identity/v1.7.0/deploy/infra/deployment.yaml

# Wait up to 30 seconds before deploying to make sure the required roles etc. exist

# Indicate that MIC and NMI should bypass the interception
kubectl apply -f https://raw.githubusercontent.com/Azure/aad-pod-identity/master/deploy/infra/mic-exception.yaml

```

### Notes

This leverages bash to perform the role provisioning based on the pod identity repository.  All commands unless otherwise noted assume kubectl is configured correctly and execution starts in the root of the GitHub repository.



## Creating and Assigning a Pod Identity

This demonstrates how to create a managed identity for use by a pod and assign it using labels.

### The Code

```bash
resourceGroup={resource group}
identityName={name of identity}
subscriptionId={subscription id}

az identity create -g $resourceGroup -n $identityName
export IDENTITY_CLIENT_ID="$(az identity show -g $resourceGroup -n $identityName --query clientId -otsv)"
export IDENTITY_RESOURCE_ID="$(az identity show -g $resourceGroup -n $identityName --query id -otsv)"

# Assign role reader rights to the identity so it can access the resource group
export IDENTITY_ASSIGNMENT_ID="$(az role assignment create --role Reader --assignee ${IDENTITY_CLIENT_ID} --scope /subscriptions/$subscriptionId/resourceGroups/$resourceGroup --query id -otsv)"

# Create a YAML file named createidentity.yaml to create the identity with contents seen here

# apiVersion: "aadpodidentity.k8s.io/v1"
# kind: AzureIdentity
#metadata:
#  name: ${IDENTITY_NAME}
# spec:
#  type: 0 # Indicates system assigned managed identity is being used
#  resourceID: ${IDENTITY_RESOURCE_ID}
#  clientID: ${IDENTITY_CLIENT_ID}

kubectl apply -f ./createidentity.yaml

# Create a YAML file named bindidentity.yaml to bind the id

# apiVersion: "aadpodidentity.k8s.io/v1"
# kind: AzureIdentityBinding
# metadata:
#   name: ${IDENTITY_NAME}-binding
# spec:
#   azureIdentity: ${IDENTITY_NAME}
#   selector: ${IDENTITY_NAME}

kubectl apply -f ./bindidentity.yaml

# When deploying a Pod to use this identity accessing items apply the aadpodidbinding value as follows

apiVersion: v1
kind: Pod
metadata:
  name: demo
  labels:
    aadpodidbinding: $IDENTITY_NAME
  spec:
    containers:
    - name: demo
      image: mcr.microsoft.com/oss/azure/aad-pod-identity/demo:v1.7.0
      args:
        - --subscriptionid=${SUBSCRIPTION_ID}
        - --clientid=${IDENTITY_CLIENT_ID}
        - --resourcegroup=${IDENTITY_RESOURCE_GROUP}
      env:
        - name: MY_POD_NAME
          valueFrom:
            fieldRef:
              fieldPath: metadata.name
        - name: MY_POD_NAMESPACE
          valueFrom:
            fieldRef:
              fieldPath: metadata.namespace
        - name: MY_POD_IP
          valueFrom:
            fieldRef:
              fieldPath: status.podIP


```

### Notes

All commands unless otherwise noted assume kubectl is configured correctly and execution starts in the root of the GitHub repository.







