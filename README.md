# Bookstore Management System (.NET Modular MVC)

A modular ASP.NET Core MVC application that demonstrates:

- Clean separation between modules (Catalog, Orders)
- Real-world business logic (order processing with stock validation)
- Unit testing with mocks (no database dependency)

---

## 🚀 Tech Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (SQL Server)
- Razor Class Library (modular UI)
- xUnit + Moq + FluentAssertions (Unit Testing)

---

## 🧱 Architecture

```text
Modules (Catalog, Orders)
        ↓
Infrastructure (DbContext, Repositories)
        ↓
WebHost (MVC UI)
```

- **Modules**: business logic & domain models (no dependency on Infrastructure)
- **Infrastructure**: EF Core, data access
- **WebHost**: UI, routing, DI

---

## 📦 Features

### Catalog

- Manage Books (CRUD)
- Manage Categories (CRUD)
- Soft delete via `IsActive`

### Orders

- Create order with stock validation
- Prevent ordering inactive/out-of-stock books
- Cancel order → restore stock

---

## 🧠 Business Rules (Highlights)

- Cannot create order if stock is insufficient
- Cannot create order for inactive books
- Cancelling an order restores stock
- Order total is calculated from items (Quantity × UnitPrice)

---

## 🧪 Unit Tests

- Focused on testing business logic in `OrderService`
- Used **Moq** to mock repositories (no real database)
- Ensured:
  - Stock decreases when an order is created
  - Order is not created when stock is insufficient
  - Stock is restored when the order is cancelled
  - Cannot cancel an already cancelled order
- Covered:
  - Create success → stock decreases
  - Create fail → no DB write
  - Cancel → stock restored
  - Cancel twice → fail

Run tests:

```bash
dotnet test
```

## ▶️ How to Run

```bash
dotnet ef database update --project src/Bookstore.Infrastructure --startup-project src/Bookstore.WebHost
dotnet run --project src/Bookstore.WebHost
```

## 🎯 Why This Project

This project demonstrates my ability to:

- Design modular architecture in .NET
- Implement real business rules beyond CRUD
- Write unit tests to ensure code reliability
- Work with Entity Framework Core and dependency injection
