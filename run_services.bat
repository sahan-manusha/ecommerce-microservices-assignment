@echo off
echo Starting MTIT Assignment 2 Microservices...
echo.

echo Starting Product Service (Port 5001)...
start "Product Service (Member 1)" cmd /k "title Product Service (5001) && cd ProductService && dotnet run"

echo Starting Order Service (Port 5002)...
start "Order Service (Member 2)" cmd /k "title Order Service (5002) && cd OrderService && dotnet run"

echo Starting Customer Service (Port 5003)...
start "Customer Service (Member 3)" cmd /k "title Customer Service (5003) && cd CustomerService && dotnet run"

echo Starting Inventory Service (Port 5004)...
start "Inventory Service (Member 4)" cmd /k "title Inventory Service (5004) && cd InventoryService && dotnet run"

echo Starting API Gateway (Port 5000)...
start "API Gateway (Ocelot)" cmd /k "title API Gateway (5000) && cd ApiGateway && dotnet run"

echo.
echo All services have been launched in separate windows!
echo Please wait a few seconds for all of them to build and start.
echo.
echo You can now access:
echo API Gateway: http://localhost:5000/swagger
echo.
pause
