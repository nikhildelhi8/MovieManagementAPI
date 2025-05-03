# 🎬 Movie Management API

A RESTful Web API built with **ASP.NET Core** that allows users to manage a collection of movies, directors, and detailed metadata. The API includes secure authentication, role-based access, versioning, and interactive documentation with Swagger.

---

## 📌 Features

- ✅ **CRUD Operations** for Movies and Directors  
- 🔐 **JWT Authentication** for secure access  
- 🛡️ **Role-Based Authorization** (e.g., Admin-only routes)  
- 📖 **API Versioning** to manage evolving endpoints  
- 📝 **Swagger UI** with XML comments for clear documentation  
- 🗂️ **Entity Framework Core** for ORM and database operations  
- 🔄 **AutoMapper** for mapping DTOs and Entities  

---

## 🛠️ Tech Stack

- **ASP.NET Core Web API**
- **Entity Framework Core**
- **AutoMapper**
- **Swagger (Swashbuckle)**
- **SQL Server** (or In-Memory for testing)
- **JWT Bearer Tokens**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use in-memory DB)
- [Postman](https://www.postman.com/) or Swagger UI for testing

### Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/movie-management-api.git

# Navigate to the project folder
cd movie-management-api

# Restore dependencies
dotnet restore

# Run the project
dotnet run
