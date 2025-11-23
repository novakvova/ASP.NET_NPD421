@echo off

echo Changing directory api...
cd "WebApiATB"

echo Building Docker image api...
docker build -t api-npd421-api .

echo Docker login...
docker login

echo Tagging Docker image api...
docker tag api-npd421-api:latest novakvova/npd421-api:latest

echo Pushing Docker image api to repository...
docker push novakvova/npd421-api:latest

echo Done ---api---!
pause

