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









