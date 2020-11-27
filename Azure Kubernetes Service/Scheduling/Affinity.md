# Scheduling Pods

Pod scheduling is important to understand the concepts of whether it is to target a specific node pool or to distribute the nodes evenly. Some of the more common scheduling operations that are beyond controllers are outlined here.



# Targeting a Node Pool

## Determining What to Target

### The Code

```Kubernetes CLI
# Get the available nodes
kubectl get nodes

# Sample Output
# NAME                                 STATUS   ROLES   AGE   VERSION
# aks-agentpool-32092124-vmss000000    Ready    agent   24h   v1.18.10
# aks-agentpool-32092124-vmss000001    Ready    agent   24h   v1.18.10
# aks-agentpool-32092124-vmss000002    Ready    agent   24h   v1.18.10
# aks-processors-32092124-vmss000000   Ready    agent   24h   v1.18.10
# aks-processors-32092124-vmss000001   Ready    agent   24h   v1.18.10
# aks-processors-32092124-vmss000002   Ready    agent   24h   v1.18.10

# To target the processors node pool inspect one of the nodes prefixed with aks-processors
kubectl describe node aks-processors-32092124-vmss000000

# Reviewing the output the label named agentpool indicates the pool name so will be the target
# Name:               aks-processors-32092124-vmss000001
#Roles:              agent
#Labels:             agentpool=processors
#                    beta.kubernetes.io/arch=amd64
#                    beta.kubernetes.io/instance-type=Standard_D8s_v3
#                    beta.kubernetes.io/os=linux

```

### Notes



## Targeting the Node

To target the node the YAML file can be adjusted to include an affinity definition as seen in the StatefulSet definition below.

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
      affinity:
        nodeAffinity:
          requiredDuringSchedulingIgnoredDuringExecution:
            nodeSelectorTerms:
            - matchExpressions:
              - key: agentpool
                operator: In
                values:
                - processors
      serviceAccountName: demosrvact
      containers:
      - name: legacy-eph
        image: cgcdns.azurecr.io/testcontainer:001

```

### Notes

Note that this is handled through nodeAffinity, not podAffinity.



# Distributing Pods Evenly

## Anti-affinity

To distribute the pods evenly across the nodes it requires anti-affinity rules to prefer they are not collocated. The code leverages the preferred syntax instead of required to allow for multiples to be added to the same when it cannot be avoided. This attempts to split any pods that have a label of ephinstance of demo onto their own nodes based on the hostname.  

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
        ephinstance: demo
    spec:
        podAntiAffinity:
          preferredDuringSchedulingIgnoredDuringExecution:
          - podAffinityTerm:
              labelSelector:
                matchLabels:
                  ephinstance: demo
              namespaces:
              - ephdemo
              topologyKey: kubernetes.io/hostname
            weight: 100
      serviceAccountName: demosrvact
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

```

### Notes

Note that this is handled through podAffinity, not nodeAffinity.













