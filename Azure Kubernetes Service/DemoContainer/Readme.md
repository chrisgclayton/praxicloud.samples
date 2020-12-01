# Samples: Demo Container

PraxiCloud samples offer samples of using the components in the PraxiCloud libraries and or common tasks that benefit from a simple, single location description, and or how to. The Demo Container sample creates a Kubernetes ReplicaSet based on the PraxiCloud container base class. This demonstration implements availability, liveness, and controller scale callbacks. 

At the end the prometheus endpoint for scraping can be queried to demonstrate the entire piece as well as metrics.


# Building the Container

## Building the Docker Image

### The Code

```Docker CLI
# Using the password is used for test purposes only
docker login -u {ACR User Name} -p {ACR Password} {ACR Login Server}

# From the root of the sample where the Dockerfile is located
docker build -t {ACR Login Server}/democontainer:001 .
docker push {ACR Login Server}/democontainer:001
```

### Notes

Execute the commands from the root of the sample directory.



## Updating the YAML Files

The replicaset.yaml files found in the deployment directory must be updated with the appropriate information for the deployment environment, such as docker image name. 

Find the {Load Balancer Public IP Address} text and replace the entire block with the IP Address that was created for associating with load balanced resources. This is the public IP Address that the container will be available on.

Find the {Image Name} text and replace it with the image name of the docker image that was built in the last step ({ACR Login Server}/democontainer:001).


# Deploying to the Cluster

## Deployment Steps

From the deployment directory run the following commands.

### The Code

```Kubernetes CLI
# Creating a separate namespace to deploy to 
kubectl create namespace demonamespace

# The rights assignments were broken out and are to allow for API access to detect scale events
kubectl apply -f queryrights.yaml
kubectl apply -f replicaset.yaml

# Check when an external IP address has been assigned
kubectl get services -n demonamespace
```

### Notes

The above steps assume that the kubectl context has already been set to the AKS cluster being deployed to. 

Once the services external endpoint has been assigned the testing step can be done to validate.



# Viewing the Prometheus Endpoint

## Accessing the Scraping Endpoint

The demo container uses metrics to record each iteration of 15 seconds that it successfully completes. This counter can be seen on the scraped Prometheus endpoint and is identified as demo_iteration_counter. 

Using the web browser of choice access the following endpoint, replaceing {Load Balancer Public IP Address} with the IP Address returned as the external IP in the services query.

http://{Load Balancer Public IP Address}:9600/metrics

The resulting page should look similar to the following.

\# HELP process_working_set_bytes Process working set
\# TYPE process_working_set_bytes gauge
process_working_set_bytes 103354368
\# HELP dotnet_collection_count_total GC collection count
\# TYPE dotnet_collection_count_total counter
dotnet_collection_count_total{generation="1"} 11
dotnet_collection_count_total{generation="2"} 1
dotnet_collection_count_total{generation="0"} 23
\# HELP process_cpu_seconds_total Total user and system CPU time spent in seconds.
\# TYPE process_cpu_seconds_total counter
process_cpu_seconds_total 394.57
\# HELP dotnet_total_memory_bytes Total known allocated memory
\# TYPE dotnet_total_memory_bytes gauge
dotnet_total_memory_bytes 23322712
**\# HELP demo_iteration_counter Counts the number of iterations the demo container has performed.**
**\# TYPE demo_iteration_counter counter**
**demo_iteration_counter 2200**
\# HELP process_virtual_memory_bytes Virtual memory size in bytes.
\# TYPE process_virtual_memory_bytes gauge
process_virtual_memory_bytes 3995148288
\# HELP process_num_threads Total number of threads
\# TYPE process_num_threads gauge
process_num_threads 14
\# HELP process_start_time_seconds Start time of the process since unix epoch in seconds.
\# TYPE process_start_time_seconds gauge
process_start_time_seconds 1606787324.47
\# HELP process_private_memory_bytes Process private memory size
\# TYPE process_private_memory_bytes gauge
process_private_memory_bytes 174882816
\# HELP process_open_handles Number of open handles
\# TYPE process_open_handles gauge
process_open_handles 153

### Notes

The endpoint is exposed to the public which is only done for this sample and instead for any real usage should be limited to lower exposure port type. 









