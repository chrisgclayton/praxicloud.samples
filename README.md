# PraxiCloud Samples
PraxiCloud Libraries are a set of common utilities and tools for general software development that simplify common development efforts for software development. The samples repository is used to provide examples on how to use these libraries and tools, or perform common tasks as an easy point of reference.



# Azure Kubernetes Services ("AKS")

Examples that fall into this include common scripts and activities, or building pods and containers.

## Samples

In these samples anytime an AKS instance is expected to be in existence it is based on an environment starting with the configuration that can be found [here](Azure%20Kubernetes%20Service/Configuring%20the%20cluster.md).

|Sample Name| Description | Notes |
| ------------- | ------------- | ------------- |
|**Kubectl Configuration**| Demonstrates common kubectl actions when working with AKS including configuring access to the cluster.<br />**Location** [Kubectl.md](Azure%20Kubernetes%20Service/Configuration/Kubectl.md) |  |
|**Azure Container Registry**| How to leverage Azure Container Registry with Azure Kubernetes Service, whether being set as the default for the cluster or one off image access.<br />***Location*** [Azure Container Registry.md](Azure%20Kubernetes%20Service/Configuration/Azure%20Container%20Registry.md) | |
|**InitializeDashboard**|Enables the Kubernetes dashboard on an RABAC enabled cluster, as well as retrieval of the token to access it.<br>***Location*** [Dashboard.md](Azure%20Kubernetes%20Service/Configuration/Dashboard.md)|  |
|**Pod Scheduling**|Demonstrates how to perform scheduling tasks such as balancing pods evenly across nodes using (anti-)affinity, and targeting a specific node pool.<br/>***Location*** [Affinity.md](Azure%20Kubernetes%20Service/Scheduling/Affinity.md)| |
|**Taints and Tolerations**|Although technically part of scheduling this sample describes how to add taints to nodes and identify pods as tolerating thing.<br/>***Location*** [Affinity.md](Azure%20Kubernetes%20Service/Scheduling/Taints.md)| |
|**Configuring Auto-scaling**|Configures a Kubernetes cluster to auto-scale using the cluster auto-scaler and pods using the horizontal pod scaler.<br/>***Location*** [Auto Scaling.md](Azure%20Kubernetes%20Service/Scaling/Auto%20Scaling.md)| |
|**Configuring Managed Service Identities for Pods**|Configuring Azure Managed Service Identity for pods in the cluster to access Azure resources.<br/>***Location*** [Pod Identities.md](Azure%20Kubernetes%20Service/Configuration/Pod%20Identities.md)| |
|**Exposing a Service Publicly**|Exposing a service publicly can be done using layer 4 or layer 7 of the OSI. <br />***Location*** [Ingress.md](Azure%20Kubernetes%20Service/Ingress/Ingress.md)| |

## Additional Information

For additional information look for readme files in each of the samples directories.