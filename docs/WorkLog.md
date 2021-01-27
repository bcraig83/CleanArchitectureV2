# 2021-01-25 (Session 2)

## Summary from session 1

- I discussed the application and how we use Rabbit MQ
- I demonstrtated how I test that everything is getting wired up correctly.
- I refactored the code on-the-fly, as I came up with improvements.
- I put the application into a docker-compose file and proved that all worked.
- I installed minikube and had started playing around with it.
- I ran Kompose to generate K8S yaml files.

## The goals for session 2 are

- Get a k8s cluster running locally - DONE. (Started with minikube, switched to docker for desktop.)
- Deploy my application to that cluster and prove that it works - DONE
- Deploy my application to an instance of Azure K8S Service (AKS) and prove that it works
- Add Azure Service Bus?

## Notes

Producer --> RabbitMQ --> Consumer


Code Locally --> Push to git repo --> Publish docker image --> Azure Docker Registry --> Deploy image(s) to K8S



Use minikube docker daemon and registry:
>@FOR /f "tokens=*" %i IN ('minikube -p minikube docker-env') DO @%i


Possibly uninstall minikube to avoid any conflicts in the future.



To start k8s proxy:

kubectl proxy






az aks get-credentials --resource-group rg-bookworm --name aks-bookworm


## Review

- What went well:
    - Docker for desktop K8S implementation
    - Talking through the blockers and rewatching the video of those blockers, helped solve them.
    - Recording helped keep focus and motivation.
    - Got the local cluster working
    - Apply / delete scripts (can be improved)
- What didn't go so well
    - Minikube
    - A bit painful grabbing logs from k8s
    - My crummy internet speed, esp upload
    - Might need to tidy up our yaml files.
    - Neglected updates to readme.
    - Lack of TDD.
    - Need to take the time to properly work through code changes (compile, test, etc)

# 2021-01-26 (Session 3)

## Goals

- Deploy my application to an instance of Azure K8S Service (AKS) and prove that it works
    - Be able to access "health" endpoint from curl command
- Add Azure Service Bus


## Notes

http://51.104.175.97:7008/api/Books/health


Internet --> Ingress Controller --> Cluster --> Service



helm install nginx-ingress ingress-nginx/ingress-nginx --set controller.replicaCount=1 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux --set controller.admissionWebhooks.patch.nodeSelector."beta\.kubernetes\.io/os"=linux


kubectl get services -o wide -w nginx-ingress-ingress-nginx-controller

Maybe expose the deployments using "NodePort" service for now? It's rough and ready, but it would work...
Actually..


## Ideas

### Be able to access "health" endpoint from curl command / Postman

- I could try accessing that endpoint using "curl" from another pod?

