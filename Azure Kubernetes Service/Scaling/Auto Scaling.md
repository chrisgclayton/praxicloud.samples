# Kubernetes Auto-scaling
These samples show how to configure both the Kubernetes Pod Auto-scaler and Node Auto-scalers in Azure Kubernetes Services. Although this focuses on AKS the mechanism is common to Kubernetes.



# Scaling the Cluster

## Configuring an Existing Cluster for Auto Scaling of a Node Pool

### The Code
```Azure CLI
# Set autoscaling on for the cluster
az aks update --resource-group myResourceGroup --name myAKSCluster --enable-cluster-autoscaler

# With multiple node pools it will indicate to use the nodepool command to configure, as follows
az aks nodepool update --resource-group {Cluster Resource Group} --cluster-name {Cluster Name} --name {Node Pool Name} --enable-cluster-autoscaler --min-count {Minimum number of Nodes} --max-count {Maximum number of Nodes}
```
### Notes

The decision to scale nodes are made when pods cannot be scheduled.



# Scaling Pods

Pod scaling is based on the availability and use of metrics provided by the Kubernetes Metric Server. In recent versions of Azure Kubernetes Services this is installed by default, previous versions will require manual steps to configure it. 

## Configuring Pods to Report Metrics

To make optimal use of the Pod Auto-scaler it is important for pods to report metrics that are important to them with the typical being CPU and Memory. It is a best practice for each pod to define its limits and requests for resources, including at a minimum CPU and Memory. The following code demonstrates how to configure the requests and limits within a Stateful Set.

### The Code

```Yaml
apiVersion: apps/v1
kind: StatefulSet
metadata:
  name: eph-legacy-statefulset
  namespace: ephdemo
spec:
  serviceName: eph-demo-service
  replicas: 2
  selector:
    matchLabels:
      app: demo-legacy-processor
      tech: event-processor-host
      component: demo-eph
  template:
    metadata:
      labels:
        app: demo-legacy-processor
        tech: event-processor-host
        component: demo-eph
    spec:
      containers:
      - name: legacy-eph
        image: cgcdns.azurecr.io/testcontainer:001
        resources:
          requests:          
            cpu: 100m
            memory: 128Mi
          limits:          
            cpu: 250m
            memory: 256Mi
      restartPolicy: Always
```

### Notes

The resource definitions are not limited to CPU and memory, although they are the most common.



## Configuring Horizontal Pod Auto-scaler Using the Kubernetes CLI

There are several ways to configure the Kubernetes Horizontal Pod Auto-scaler. This sample will configure a the azure-vote-frontend to scale elastically between 3 and 10 nodes when the total CPU consumed across all nodes reaches 50%.

### The Code

```Kubernetes CLI
kubectl autoscale deployment azure-vote-front --cpu-percent=50 --min=3 --max=10
```

### Notes

It is recommended that the auto-scaling being used in conjunction with anti affinity patterns if the desire is to distribute the pods as they are scheduled across the available nodes. If this is not done compute nodes may sit idle.



## Configuring Horizontal Pod Auto-scaler Using YAML

There are several ways to configure the Kubernetes Horizontal Pod Auto-scaler. This sample will configure a the azure-vote-frontend to scale elastically between 3 and 10 nodes when the total CPU consumed across all nodes reaches 50%.

### The Code

```Yaml
apiVersion: autoscaling/v1
kind: HorizontalPodAutoscaler
metadata:
  name: azure-vote-back-hpa
spec:
  maxReplicas: 10 # define max replica count
  minReplicas: 3  # define min replica count
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: azure-vote-front
  targetCPUUtilizationPercentage: 50 # target CPU utilization
```

### Notes

It is recommended that the auto-scaling being used in conjunction with anti affinity patterns if the desire is to distribute the pods as they are scheduled across the available nodes. If this is not done compute nodes may sit idle. It is important to also consider scale down behaviors, which can be set using stabilizationWindowSeconds.



## Retrieving the Current Details of the Horizontal Pod Scaling

Retrieving the details of the horizontal pod scaler's current node count etc.

### The Code

```bash
kubectl get hpa
```

### Notes

N/A

