# comando para observar a quantidade de pods:

kubectl get pods -n techfood

kubectl get deployments techfood-api -n techfood

kubectl logs -f 'nomedopod' -n techfood

apply novo hpa:http://localhost:30000/api/v1/Menu

kubectl get hpa -n techfood

kubectl logs -f techfood-admin-8486cdff74-b58fx -n techfood
