# Kubectl Configuration
This sample demonstrates the commands and process to perform some of the more common kubectl configuration commands used when working with Azure Kubernetes Services.



# Configuring and Setting Contexts

## Configuring Access

### The Code
```Azure CLI
az login
az account set --subscription {subscription name or id}
az aks get-credentials --resource-group {cluster resource group} --name {cluster name}
```
### Notes

This will add a context and cluster details to the kubectl configuration file, located at ~/.kube/config in bash or \Users\\{user name}}\\.kube\\config in windows.



## Viewing Available Contexts

### The Code

```Docker CLI
kubectl config get-contexts
```

The output will look similar to the following, with the * beside the currently selected configuration.

![image-20201126063408070](C:\Users\cclayton\AppData\Roaming\Typora\typora-user-images\image-20201126063408070.png)

### Notes

It is important to remember all actions performed with kubectl at this point will be directed by default at the configuration with the * beside it.



## Setting the Current Context

### The Code

```Docker CLI
kubectl config use-context {context name}
```


### Notes

It is important to remember all actions performed with kubectl at this point will be directed to the context identified in the command.



# The Kubernetes Dashboard

Demonstrates the configuration and accessing of the Kubernetes dashboard.



# Configuring the Dashboard

## Configuring for RBAC Enabled

### The Code

```Kubernetes CLI
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.0.0/aio/deploy/recommended.yaml
kubectl create serviceaccount dashboard -n default
kubectl create clusterrolebinding dashboard-admin -n default --clusterrole=cluster-admin --serviceaccount=default:dashboard
```

### Notes

Before running these commands ensure that the kubectl configuration is set to the desired context.



# Accessing the Dashboard

## Retrieving the Token to Access the Dashboard

Demonstrates how to retrieve the access token that can be used to access the dashboard. This token is entered into the login screen of the dashboard when first accessing it.

### The Code

```Kubernetes CLI
# Retrieve the dashboards service account secret
kubectl get serviceaccount dashboard -o jsonpath="{.secrets[0].name}"

# Use the output from the above command as the secret name
kubectl get secret {secret name} -o jsonpath="{.data.token}"

# The output of the above command is the base 64 encoded command. Decode to use. 

# For bash replace {base 64 string} with the output of the above command
echo {base 64 string} | base64 --decode

# For Windows replace {base 64 string} with the output of the above command
powershell [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String('{base 64 string}'))


```

### Notes

Before running these commands ensure that the kubectl configuration is set to the desired context. 



## Displaying the Dashboard

Demonstrates how to retrieve the access token that can be used to access the dashboard. This token is entered into the login screen of the dashboard when first accessing it.

After executing the code below ("kubectl proxy") navigate to the dashboard through the proxy using a browser.

http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/

### The Code

```Kubernetes CLI
# This will continue to run so start it in an unused console / terminal
kubectl proxy
```

### Notes

Before running these commands ensure that the kubectl configuration is set to the desired context. If the login screen is not displayed log out first using the top right corner









