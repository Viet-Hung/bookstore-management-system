# Bookstore-management-system

Modular ASP.NET Core MVC bookstore management system demonstrating clean architecture, layered design, and real-world backend practices.

The project implements a complete flow from UI → Service → Repository → Database using Entity Framework Core and SQL Server.

## Features

### Catalog Management

- View list of books
- Create new book with form validation
- Category-based organization

### Validation

- Server-side validation using Data Annotations
- Client-side validation using jQuery Unobtrusive Validation
- Proper error handling and user feedback

### Architecture

- Layered architecture (Controller → Service → Repository)
- Separation of concerns
- Dependency Injection

### Database

- Entity Framework Core (Code First)
- SQL Server integration
- Migrations and data seeding

## Project Structure

src/

- Bookstore.WebHost → ASP.NET Core MVC (UI layer)
- Bookstore.Infrastructure → EF Core, DbContext, Repository implementations
- Modules/
  - Bookstore.Module.Core → Shared UI components
  - Bookstore.Module.Catalog → Book & Category domain logic
  - Bookstore.Module.Orders → (in progress)
  - Bookstore.Module.Users → (in progress)

## How to Run

1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run database migration:

   dotnet ef database update --project src/Bookstore.Infrastructure --startup-project src/Bookstore.WebHost

4. Run the application:

   dotnet run --project src/Bookstore.WebHost

## Key Highlights

- Clean separation between layers (Controller, Service, Repository)
- Modular architecture with feature-based modules
- Real database integration (not in-memory)
- Form validation (client + server)
- Production-style project structure
