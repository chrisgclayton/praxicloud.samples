# Azure Kubernetes Service Ingress
There are multiple ingress options with Kubernetes that target different features, functions and OSI layers. 



# OSI Layer 4 Public Exposure

## Basic Exposure

To start exposing as a basic Load Balanced layer 4 create a Public IP Address in the resource group named MC\_{Cluster Resource Group Name}\_{Cluster Name}\_{Location}. In this scenario one was created named externalip as a standard SKU. Note the IP Address and DNS name for future use.

### The Code

```Yaml
# Create a Load Balanced service that uses the public IP Address

apiVersion: v1
kind: Service
metadata:
  name: eph-demo-service
  namespace: ephdemo
  labels:
      app: demo-legacy-processor
      component: demo-eph
spec:
  loadBalancerIP: {Your IP Address}
  type: LoadBalancer
  ports:
  - port: 10080
    name: srvval
  selector:
      app: demo-legacy-processor
      component: demo-eph
```
### Notes

This will take some time before the service is associated with the IP Address. 



## Confirm the External IP Address and Ports are Assigned

### The Code

```Kubernetes CLI
kubectl get services -n {Service Namespace}

# Sample Output Noting External-IP will show when associated
# NAME               TYPE           CLUSTER-IP   EXTERNAL-IP     	 PORT(S)           AGE
# eph-demo-service   LoadBalancer   10.0.98.93   {Your IP Address}   10080:31661/TCP   43h
```

### Notes

This will take some time before the service is associated with the IP Address but running this command will confirm when it shows up. If an DNS name was provided it can be used to access the service as well as IP. A new Load Balancer in Azure is created for this service.





