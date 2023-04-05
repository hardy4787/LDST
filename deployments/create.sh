kubectl apply -f postgres-configmap.yaml 
kubectl apply -f postgres-pvc-pv.yaml 
kubectl apply -f postgres-deployment.yaml 
kubectl apply -f postgres-service.yaml 
kubectl apply -f backend-deployment.yaml 
kubectl apply -f backend-service.yaml 
kubectl apply -f ingress-api.yaml 