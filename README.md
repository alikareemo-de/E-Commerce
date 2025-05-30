# 🛒 E-Commerce Order Management Backend

This project is a backend service for managing customer orders in a small e-commerce platform, built with C# using **Domain-Driven Design (DDD)** principles, **MediatR**, and a **State Machine** to control the order lifecycle.

---

## ✅ Features

- Domain modeling using **DDD**
- State machine for order workflow using **Stateless**
- Command/Query separation via **MediatR**
- Clean layered architecture:
  - Domain
  - Application
  - Infrastructure
  - API
- **Domain Events** with handlers
- Unit tests using **xUnit**
- Lightweight persistence using **SQLite**

---

## 📦 Order Lifecycle (State Machine)

| Current Status | Allowed Transitions        |
|----------------|-----------------------------|
| `Pending`      | `Confirm`, `Cancel`         |
| `Confirmed`    | `Ship`, `Cancel`            |
| `Shipped`      | `Deliver`                   |
| `Delivered`    | _(final)_                   |
| `Cancelled`    | _(final)_                   |

Implemented with the [Stateless](https://github.com/dotnet-state-machine/stateless) library.

---

## 🚀 API Endpoints

| Method | Endpoint               | Description          |
|--------|------------------------|----------------------|
| POST   | `/orders`              | Create a new order   |
| GET    | `/orders/{id}`         | Get order details    |
| DELETE | `/orders/{id}`         | Cancel an order      |

Additional transitions:
- `POST /orders/{id}/confirm`
- `POST /orders/{id}/ship`
- `POST /orders/{id}/deliver`

---

## 🧪 Unit Testing

Tests included:
- Order creation
- Order lifecycle transitions
- Invalid transitions (e.g. cancel shipped order)
- Domain event triggering

Run tests via:

```bash
dotnet test


 Project Structure

 /src
  /ECommerce.Domain          -- Entities, ValueObjects, Aggregates, Domain Events
  /ECommerce.Application     -- Commands, Handlers, DTOs, Events
  /ECommerce.Infrastructure  -- EF Core context, Repositories
  /ECommerce.API             -- Controllers, DI config, Swagger

/tests
  /ECommerce.Tests           -- Unit & integration tests

 Tech Stack
 .NET 8

C#

EF Core + SQLite

MediatR

Stateless (for state machine)

xUnit (for testing)

✍️ Author
Ali Kareemo
GitHub Profile



