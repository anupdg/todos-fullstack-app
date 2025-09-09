@echo off
echo ================================
echo Todos Application Docker Status
echo ================================
echo.

echo Checking if containers are running...
docker ps --filter "label=com.todos.group=todos-app" --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
echo.

echo Checking network connectivity...
docker network inspect todos-network --format "{{json .Containers}}" 2>nul
echo.

echo Testing API health...
curl -s http://localhost:5000/health | echo API Health Check Response:
echo.

echo Testing frontend accessibility...
curl -s -I http://localhost:3000 | findstr "HTTP"
echo.

echo ================================
echo Service URLs:
echo API: http://localhost:5000/graphql
echo Frontend: http://localhost:3000
echo ================================
