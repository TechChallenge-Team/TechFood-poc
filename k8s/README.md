# TechFood - Kubernetes Deployment

Este diretório contém os manifestos e scripts necessários para fazer o deploy da aplicação TechFood no Kubernetes usando Minikube.

## 📋 Pré-requisitos

- [Minikube](https://minikube.sigs.k8s.io/docs/start/) instalado e configurado
- [kubectl](https://kubernetes.io/docs/tasks/tools/) instalado
- [Docker](https://docs.docker.com/get-docker/) instalado
- Mínimo de 4GB de RAM disponível para o Minikube

## 🚀 Deploy Rápido

### 1. Iniciar o Minikube

```bash
minikube start --memory=4096 --cpus=2
```

### 2. Build das Imagens

Execute o script para fazer build das imagens Docker:

```bash
# Windows
k8s\build-images.bat

# Linux/Mac
./k8s/build-images.sh
```

### 3. Deploy da Aplicação

Execute o script de deploy:

```bash
# Windows
k8s\deploy.bat

# Linux/Mac
./k8s/deploy.sh
```

### 4. Validar o Deploy

```bash
# Windows
k8s\validate.bat

# Linux/Mac
./k8s/validate.sh
```

### 5. Acessar a Aplicação

```bash
minikube service techfood-nginx-service -n techfood
```

### 6. Limpeza (Opcional)

```bash
# Windows
k8s\cleanup.bat

# Linux/Mac
./k8s/cleanup.sh
```

## 🏗️ Arquitetura

A aplicação TechFood é composta pelos seguintes componentes:

### Frontend Applications

- **Admin**: Interface administrativa para gerenciar produtos e pedidos
- **Self-Order**: Interface de autoatendimento para clientes
- **Monitor**: Dashboard para monitoramento de pedidos

### Backend Services

- **API**: API REST principal da aplicação
- **Database**: SQL Server para persistência de dados
- **Nginx**: Reverse proxy e load balancer

### Infraestrutura Kubernetes

- **Namespace**: `techfood` - Isolamento dos recursos
- **ConfigMaps**: Configurações não sensíveis
- **Secrets**: Dados sensíveis (senhas, tokens)
- **PersistentVolumes**: Armazenamento persistente
- **Services**: Exposição interna dos serviços
- **Deployments**: Gerenciamento dos pods
- **HPA**: Auto-escalabilidade baseada em CPU/memória

## 📊 Escalabilidade (HPA)

A aplicação está configurada com Horizontal Pod Autoscaler:

| Componente | Min Replicas | Max Replicas | CPU Target | Memory Target |
| ---------- | ------------ | ------------ | ---------- | ------------- |
| API        | 2            | 10           | 70%        | 80%           |
| Self-Order | 3            | 15           | 70%        | 80%           |
| Admin      | 2            | 5            | 70%        | 80%           |
| Monitor    | 2            | 5            | 70%        | 80%           |
| Nginx      | 2            | 5            | 70%        | 80%           |

## 🔒 Segurança

### ConfigMaps (Dados não sensíveis)

- Configurações de ambiente
- URLs e endpoints
- Configurações do Nginx

### Secrets (Dados sensíveis)

- Senhas do banco de dados
- Tokens JWT
- Chaves de API do Mercado Pago

## 📁 Estrutura dos Manifestos

```
k8s/
├── base/                           # Manifestos base
│   ├── namespace.yaml             # Namespace da aplicação
│   ├── configmaps.yaml            # Configurações não sensíveis
│   ├── secrets.yaml               # Dados sensíveis
│   ├── pvc.yaml                   # Persistent Volume Claims
│   ├── techfood-db.yaml           # Database deployment
│   ├── techfood-api.yaml          # API deployment
│   ├── techfood-admin.yaml        # Admin app deployment
│   ├── techfood-self-order.yaml   # Self-order app deployment
│   ├── techfood-monitor.yaml      # Monitor app deployment
│   ├── techfood-nginx.yaml        # Nginx deployment
│   ├── hpa.yaml                   # Horizontal Pod Autoscalers
│   └── kustomization.yaml         # Kustomize configuration
├── overlays/
│   └── development/               # Configurações para desenvolvimento
│       ├── kustomization.yaml
│       └── development-patches.yaml
├── build-images.bat              # Script para build das imagens
├── deploy.bat                    # Script de deploy
└── README.md                     # Este arquivo
```

## 🌐 Endpoints

Após o deploy, a aplicação estará disponível nos seguintes endpoints:

- **Admin**: http://localhost:30000/admin
- **Self-Order**: http://localhost:30000/self-order
- **Monitor**: http://localhost:30000/monitor
- **API**: http://localhost:30000/api
- **Health Check**: http://localhost:30000/health

## 📱 Comandos Úteis

### Monitoramento

```bash
# Ver todos os recursos
kubectl get all -n techfood

# Monitorar pods em tempo real
kubectl get pods -n techfood -w

# Monitorar HPA
kubectl get hpa -n techfood -w

# Ver logs de um pod
kubectl logs -f <pod-name> -n techfood

# Acessar shell de um pod
kubectl exec -it <pod-name> -n techfood -- /bin/bash
```

### Debugging

```bash
# Descrever um recurso
kubectl describe pod <pod-name> -n techfood

# Ver eventos do namespace
kubectl get events -n techfood --sort-by='.metadata.creationTimestamp'

# Port forward para um serviço
kubectl port-forward service/techfood-api-service 8080:8080 -n techfood
```

### Limpeza

```bash
# Remover todos os recursos
kubectl delete namespace techfood

# Ou usar kustomize
kubectl delete -k k8s/overlays/development/
```

## 🔧 Configuração do Minikube

### Recursos Recomendados

```bash
minikube start \
  --memory=4096 \
  --cpus=2 \
  --disk-size=20g \
  --driver=docker
```

### Addons Necessários

```bash
minikube addons enable metrics-server
minikube addons enable ingress  # Opcional
```

## 🚨 Troubleshooting

### Problemas Comuns

1. **Pods em CrashLoopBackOff**

   ```bash
   kubectl logs <pod-name> -n techfood
   kubectl describe pod <pod-name> -n techfood
   ```

2. **HPA não funciona**

   ```bash
   # Verificar se metrics-server está rodando
   kubectl get pods -n kube-system | grep metrics-server

   # Habilitar metrics-server
   minikube addons enable metrics-server
   ```

3. **Imagens não encontradas**

   ```bash
   # Verificar se está usando o Docker do Minikube
   eval $(minikube docker-env)
   docker images | grep techfood
   ```

4. **Banco de dados não conecta**

   ```bash
   # Verificar se o serviço do banco está rodando
   kubectl get svc techfood-db-service -n techfood

   # Verificar logs do banco
   kubectl logs -f deployment/techfood-db -n techfood
   ```

## 📝 Notas de Desenvolvimento

- As imagens Docker são construídas localmente no Minikube
- O banco de dados usa armazenamento persistente
- As configurações de desenvolvimento reduzem os recursos para economizar CPU/memória
- O HPA requer o metrics-server habilitado
- Por padrão, o serviço é exposto via NodePort na porta 30000

## 📚 Recursos Adicionais

- [Kubernetes Documentation](https://kubernetes.io/docs/)
- [Minikube Documentation](https://minikube.sigs.k8s.io/docs/)
- [Kustomize Documentation](https://kustomize.io/)
- [HPA Documentation](https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale/)
