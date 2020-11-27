# Azure Container Registry
This sample demonstrates ways to make use of private Azure Container Registries with the Kubernetes cluster.



# Configuring Registry Access

## Associating the Default Registry

Associating a private Azure Container Registry as the default for the cluster can be done so it will not require each deployment to provide credentials. 

### The Code

```Azure CLI
az aks update -n {cluster name} -g {cluster resource group} --attach-acr {acr name}
```
### Notes

The registry should be in the same subscription and resource group as the cluster.



## Using a Private Azure Container Registry that is not the Default

### The Code

```Docker CLI
# Create the Azure Container Registry if not already created
az acr create --name {ACR Name} --resource-group {Resource Group Name} --sku Basic

# Create a service principal
az ad sp create-for-rbac --scopes /subscriptions/{Subscription Id}/resourcegroups/{Resource Group Name}/providers/Microsoft.ContainerRegistry/registries/{ACR Name} --role Reader --name {Service Principal Name}

# The response provides the appId (client id) and password (client secret) for creating a Kubernetes secret
kubectl create secret docker-registry {secret name} --docker-server {ACR Name}.azurecr.io --docker-email {your email address} --docker-username={Service Principal Name} --docker-password {Client Secret}

# To use this secret for pulling the secrets add it to the YAML 

# apiVersion: v1
# kind: Pod
# metadata:
#   name: private-hello-world
# spec:
#   containers:
#   - name: private-hello-container
#    image: {ACR Name}.azurecr.io/hello-world
#   imagePullSecrets:
#   - name: {secret name}

```

### Notes

N/A





