# Instruções para Setup do Minikube

## 1. Instalação do Minikube

1. Baixe o arquivo .exe do [site oficial](https://minikube.sigs.k8s.io/docs/start/)
2. Adicione ao PATH do sistema

## 2. Instalação do kubectl

1. Baixe o arquivo .exe do [site oficial](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
2. Adicione ao PATH do sistema

## 3. Configuração Inicial

### Iniciar o Minikube

```powershell
minikube start --memory=4096 --cpus=2 --driver=docker
```

### Habilitar Addons Necessários

```powershell
minikube addons enable metrics-server
minikube addons enable ingress
```

## 4. Verificação da Instalação

### Verificar Status

```powershell
minikube status
kubectl cluster-info
```

### Verificar Nodes

```powershell
kubectl get nodes
```

## 5. Build e Deploy

### Fazer Build das Imagens

```powershell
cd k8s
.\build-images.bat
```

### Fazer Deploy da Aplicação

```powershell
.\deploy.bat
```

## 6. Acessar a Aplicação

### Obter URL do Serviço

```powershell
minikube service techfood-nginx-service -n techfood --url
```

### Ou abrir no Browser

```powershell
minikube service techfood-nginx-service -n techfood
```

## 7. Comandos Úteis

### Parar o Minikube

```powershell
minikube stop
```

### Deletar o Cluster

```powershell
minikube delete
```

### Dashboard do Kubernetes

```powershell
minikube dashboard
```

### Logs do Minikube

```powershell
minikube logs
```

## 8. Troubleshooting

### Problema: Minikube não inicia

```powershell
# Verificar drivers disponíveis
minikube start --help | Select-String "driver"

# Verificar perfil do Minikube
minikube profile list

# Remover e recriar o cluster
minikube delete
minikube start --driver=docker
```

### Problema: Imagens não são encontradas

```powershell
# Configurar Docker environment
minikube docker-env | Invoke-Expression

# Verificar imagens
docker images
```

### Problema: Pods em CrashLoopBackOff

```powershell
# Ver logs detalhados
kubectl logs -f <pod-name> -n techfood
kubectl describe pod <pod-name> -n techfood
```
