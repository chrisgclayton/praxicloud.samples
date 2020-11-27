# PraxiCloud Samples
PraxiCloud Libraries are a set of common utilities and tools for general software development that simplify common development efforts for software development. The samples repository is used to provide examples on how to use these libraries and tools, or perform common tasks as an easy point of reference.



# Azure Kubernetes Services ("AKS")

Examples that fall into this include common scripts and activities, or building pods and containers.

## Samples

|Sample Name| Description | Notes |
| ------------- | ------------- | ------------- |
|**Kubectl Configuration**| Demonstrates common kubectl actions when working with AKS including configuring access to the cluster.<br />**Location** ./Azure Kubernetes Service/Configuration/Kubectl.md |  |
|**Azure Container Registry**| How to leverage Azure Container Registry with Azure Kubernetes Service, whether being set as the default for the cluster or one off image access.<br />***Location*** ./Azure Kubernetes Service/Configuration/Azure Container Registry.md | |
|**InitializeDashboard**|Enables the Kubernetes dashboard on an RABAC enabled cluster, as well as retrieval of the token to access it.<br>***Location*** ./Azure Kubernetes Service/Configuration/Dashboard.md|  |
|**Pod Scheduling**|Demonstrates how to perform scheduling tasks such as balancing pods evenly across nodes using (anti-)affinity, and targeting a specific node pool.<br/>***Location*** ./Azure Kubernetes Service/Scheduling/Affinity.md| |
|**Configuring Auto-scaling**|Configures a Kubernetes cluster to auto-scale using the cluster auto-scaler and pods using the horizontal pod scaler.<br/>***Location*** [Azure Kubernetes Service/Scaling/Auto Scaling.md](https://github.com/chrisgclayton/praxicloud.samples/blob/main/Azure Kubernetes Service/Scaling/Auto Scaling.md)| |
|**Configuring Managed Service Identities for Pods**|Configuring Azure Managed Service Identity for pods in the cluster to access Azure resources.<br/>***Location*** ./Azure Kubernetes Service/Configuration/Pod Identities.md| |

## Additional Information

For additional information look for readme files in each of the samples directories.