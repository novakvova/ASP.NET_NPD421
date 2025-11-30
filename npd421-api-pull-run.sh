#!/bin/bash

set -e

server_up() {
    echo "Server up..."
    docker pull novakvova/npd421-api:latest
    docker stop npd421-api_container || true
    docker rm npd421-api_container || true
    docker run -d --restart=always -v /volumes/npd421-api/images:/app/images --name npd421-api_container -p 3089:8080 novakvova/npd421-api
	docker pull novakvova/front-npd421:latest
    docker stop front-npd421_container || true
    docker rm front-npd421_container || true
    docker run -d --restart=always --name front-npd421_container -p 3079:80 novakvova/front-npd421
}

start_containers() {
    echo "Containers start..."
    docker run -d --restart=always -v /volumes/npd421-api/images:/app/images --name npd421-api_container -p 3089:8080 novakvova/npd421-api
	docker run -d --restart=always --name front-npd421_container -p 3079:80 novakvova/front-npd421
}

stop_containers() {
    echo "Containers stop..."
    docker stop npd421-api_container || true
    docker rm npd421-api_container || true
	docker stop front-npd421_container || true
    docker rm front-npd421_container || true
}

restart_containers() {
    echo "Containers restart..."
    docker stop npd421-api_container || true
    docker rm npd421-api_container || true
    docker run -d --restart=always -v /volumes/npd421-api/images:/app/images --name npd421-api_container -p 3089:8080 novakvova/npd421-api
	docker stop front-npd421_container || true
    docker rm front-npd421_container || true
    docker run -d --restart=always --name front-npd421_container -p 3079:80 novakvova/front-npd421
}

echo "Choose action:"
echo "1. Server up"
echo "2. Containers start"
echo "3. Containers stop"
echo "4. Containers restart"
read -p "Enter action number: " action

case $action in
    1)
        server_up
        ;;
    2)
        start_containers
        ;;
    3)
        stop_containers
        ;;
    4)
        restart_containers
        ;;
    *)
        echo "Invalid action number!"
        exit 1
        ;;
esac
