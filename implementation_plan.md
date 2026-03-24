# MTIT Assignment 2 - Microservice Architecture Implementation Plan

## Overview

Design and implement a **microservice architecture** for an **E-Commerce Platform** domain. The solution includes 4 microservices (one per group member) and a central API Gateway. Each service has its own Swagger UI.

**Technology Stack:** ASP.NET Core 8 Web API (C#), Ocelot API Gateway, Swashbuckle (Swagger)

---

## Business Domain: E-Commerce Platform

| Microservice | Port | Owner |
|---|---|---|
| Product Service | 5001 | Member 1 |
| Order Service | 5002 | Member 2 |
| Customer Service | 5003 | Member 3 |
| Inventory Service | 5004 | Member 4 |
| API Gateway | 5000 | Shared |

---

## Proposed Changes

### Folder Structure

```
f:\MTIT Assignment 2\
├── ECommerceMicroservices.sln
├── ApiGateway/
├── ProductService/
├── OrderService/
├── CustomerService/
└── InventoryService/
```

---

### Product Service (Member 1)

#### [NEW] ProductService project

- **Models:** `Product` (Id, Name, Description, Price, Category)
- **Endpoints:** Full CRUD — `GET /api/products`, `GET /api/products/{id}`, `POST`, `PUT`, `DELETE`
- **Swagger UI:** Available at `http://localhost:5001/swagger`
- **Storage:** In-memory list (no database needed)

---

### Order Service (Member 2)

#### [NEW] OrderService project

- **Models:** `Order` (Id, CustomerId, ProductId, Quantity, TotalPrice, Status, OrderDate)
- **Endpoints:** Full CRUD — `GET /api/orders`, `GET /api/orders/{id}`, `POST`, `PUT`, `DELETE`
- **Swagger UI:** Available at `http://localhost:5002/swagger`
- **Storage:** In-memory list

---

### Customer Service (Member 3)

#### [NEW] CustomerService project

- **Models:** `Customer` (Id, FirstName, LastName, Email, Phone, Address)
- **Endpoints:** Full CRUD — `GET /api/customers`, `GET /api/customers/{id}`, `POST`, `PUT`, `DELETE`
- **Swagger UI:** Available at `http://localhost:5003/swagger`
- **Storage:** In-memory list

---

### Inventory Service (Member 4)

#### [NEW] InventoryService project

- **Models:** `InventoryItem` (Id, ProductId, Quantity, Warehouse, LastUpdated)
- **Endpoints:** Full CRUD — `GET /api/inventory`, `GET /api/inventory/{id}`, `POST`, `PUT`, `DELETE`
- **Swagger UI:** Available at `http://localhost:5004/swagger`
- **Storage:** In-memory list

---

### API Gateway

#### [NEW] ApiGateway project (using Ocelot)

- Routes all requests from port `5000` to the appropriate microservice
- Route pattern: `http://localhost:5000/api/products/*` → `http://localhost:5001/api/products/*`
- Swagger aggregation: Links to each service's Swagger UI from gateway

---

## Verification Plan

### Automated Tests

Each service will be tested by running it and hitting its endpoints:

```bash
# Start all services, then test:
curl http://localhost:5001/swagger        # Product Service Swagger
curl http://localhost:5002/swagger        # Order Service Swagger
curl http://localhost:5003/swagger        # Customer Service Swagger
curl http://localhost:5004/swagger        # Inventory Service Swagger
curl http://localhost:5000/api/products   # Via Gateway
curl http://localhost:5000/api/orders     # Via Gateway
curl http://localhost:5000/api/customers  # Via Gateway
curl http://localhost:5000/api/inventory  # Via Gateway
```

### Browser Verification

- Open each Swagger UI directly (ports 5001-5004) and verify endpoints work
- Open API Gateway (port 5000) and verify routing to all services
- Take screenshots for the slide deck
