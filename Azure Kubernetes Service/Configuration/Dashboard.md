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









