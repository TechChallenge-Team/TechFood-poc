@echo off
REM Script para fazer port-forward da aplicação TechFood

echo 🚀 Iniciando port-forward para acessar a aplicação TechFood...
echo.
echo ⚠️  IMPORTANTE: Este terminal deve permanecer aberto para que o port-forward funcione!
echo.
echo A aplicação estará disponível em: http://localhost:30000
echo.
echo Endpoints disponíveis:
echo - Admin: http://localhost:30000/admin
echo - Self-Order: http://localhost:30000/self-order
echo - Monitor: http://localhost:30000/monitor
echo - API: http://localhost:30000/api
echo.
echo Pressione Ctrl+C para parar o port-forward quando terminar de usar a aplicação.
echo.
pause

REM Faz port-forward do serviço nginx para a porta 30000
echo 🔗 Criando port-forward para o serviço techfood-nginx-service...
kubectl port-forward -n techfood service/techfood-nginx-service 30000:30000
