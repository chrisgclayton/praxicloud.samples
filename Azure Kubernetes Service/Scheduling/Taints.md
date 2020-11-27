# Pod Taints and Tolerances

A taint is a property that repels a pod from node or set of nodes, while a tolerance allows, but does not require, a pod to be scheduled on a node that has a matching taint. The concept is to control the types of pods that can be scheduled to run on a specific set node, or set of nodes.



# Adding a Taint to a Node	

## Adding a Taint to a Node

In this example a taint named pooltype with a value of cordoned will be applied to a node so only pods that tolerate the pooltype of cordoned can be scheduled on it. 

***NoSchedule:*** indicates that no pod can be scheduled on this node unless it has a matching toleration defined.
***PreferNoSchedule:*** Indicates that scheduling a pod on the node should not be done if possible.
***NoExecute:*** Indicates that running pods that may already be scheduled on the node should be evicted

### The Code

```Kubernetes CLI
# To add the taint
kubectl taint nodes node1 pooltype=cordoned:NoSchedule

# To view the active taints
kubectl describe node node1

# To remove the taint if desired later
# kubectl taint nodes node1 pooltype=cordoned:NoSchedule-
```

### Notes

Having a taint applied to a node does not guarantee the pod will be scheduled on the node with the taint, it only stops ones without the matching toleration from being scheduled on them.



## Adding a Toleration to a Pod

The following YAML allows a pod to be scheduled on a node with the taint pooltype of cordoned.

### The Code

```Yaml
apiVersion: v1
kind: Pod
metadata:
  name: nginx
  labels:
    env: test
spec:
  containers:
  - name: nginx
    image: nginx
    imagePullPolicy: IfNotPresent
  tolerations:
  - key: "pooltype"
    operator: "Equal"
    value: "cordoned"
    effect: "NoSchedule"
```

### Notes

N/A

















