# C# Application

## Overview
This project is a C# application built with **.NET 8** and **ASP.NET Core**, following clean architecture principles and modern enterprise development practices.


## Prerequisites
- **.NET SDK 8.0 or higher**
- **Database**: PostgreSQL or SQL Server (as configured)

Verify the installed .NET version:

```bash
dotnet --version
```

## Configuration

- **Configure the database connection string in appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string"
  }
}
```


## Database Setup

**Apply the database migrations:**
```bash
dotnet ef database update
```

## Running the Application

**Start the Web API:**
```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

**The API will be available at:**
```bash
https://localhost:5001
```

**Swagger documentation:**
```bash
https://localhost:5001/swagger
```

## Testing

**Run all unit and integration tests:**
```bash
dotnet test
```

Technology Stack

## .NET 8

- **ASP.NET Core**
- **Entity Framework Core**
- **MediatR**
- **AutoMapper**
- **FluentValidation**
