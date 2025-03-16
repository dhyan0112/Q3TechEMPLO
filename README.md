# Employee Management System API

## Features

- **CRUD Operations**: Create, Read, Update, and Delete employees.
- **Authentication**: JWT-based authentication for securing endpoints.
- **Entity Framework Core**: SQL database integration with EF Core.
- **Dependency Injection**: Modular architecture for maintainability.
- **SOLID Principles**: Ensures a scalable and maintainable codebase.
- **Swagger Documentation**: Auto-generated API documentation.

## Technologies Used

- .NET 8 Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger

```

## Installation

### Prerequisites

Ensure you have the following installed:

- .NET 8 (for swagger)
- SQL Server 
- Entity Framework Core

### Setup Instructions

Clone the repository:

```sh
git clone https://github.com/A-Hore/Q3-Employee-Management-Sysytem
cd EmployeeManagmentSystem
```

Install required dependencies:

```sh
dotnet restore
```

Add EF Core design package:

```sh
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Apply Migrations:

```sh
dotnet ef migrations add InitialCreate
```

```sh
dotnet ef database update
```

Run the API:

```sh
dotnet run
```

Open Swagger UI at:

```
https://localhost:7101/swagger/index.html
```

## API Endpoints

### Authentication

- **POST** `/api/Auth/login` - Authenticate and get JWT token

### Employee Management

- **GET** `/api/Employees` - Get all Employees
- **POST** `/api/Employees` - Create a new employee
- **PUT** `/api/Employees/{id}` - Update an employee
- **DELETE** `/api/Employees/{id}` - Delete an employee
