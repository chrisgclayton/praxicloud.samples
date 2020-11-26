# PraxiCloud Samples
PraxiCloud Libraries are a set of common utilities and tools for general software development that simplify common development efforts for software development. The samples repository is used to provide examples on how to use these libraries and tools, or perform common tasks as an easy point of reference.



# Azure Kubernetes Services ("AKS")

Examples that fall into this include common scripts and activities, or building pods and containers.

## Samples

|Sample Name| Description | Notes |
| ------------- | ------------- | ------------- |
|**Kubectl Configuration**| Demonstrates common kubectl actions when working with AKS including configuring access to the cluster.<br />**Location** |  |
|**InitializeDashboard**|Enables the Kubernetes dashboard on an RABAC enabled cluster, as well as retrieval of the token to access it.<br>***Location***|  |
|**Configuring Pod Auto-scaling**|Configures a Kubernetes pod for auto-scaling based on the specified metrics using the horizontal pod scaler.<br/>***Location***| |
|**Configuring Cluster Auto-scaling**|Configures a Kubernetes cluster to auto-scale using the cluster auto-scaler.<br/>***Location***| |
|**Target a Node Pool for Deployment of pods**|Configures the scheduling of a pod on a specific node pool.<br/>***Location***| |
|**Deploy Pods Evenly Across Nodes**|Scheduling of pods so they spread evenly across the nodes avoiding overfilling a single node with a specific pod.<br/>***Location***| |
|**Configuring Managed Service Identities for Pods**|Configuring Azure Managed Service Identity for pods in the cluster to access Azure resources.<br/>***Location***| |

## Additional Information

For additional information look for readme files in each of the samples directories.