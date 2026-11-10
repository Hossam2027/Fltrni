# Fltrni – Home Services Marketplace

Fltrni is a full-stack home services marketplace that connects customers with service providers such as carpenters, plumbers, and other professionals.

The platform allows customers to discover approved service providers, search and filter services by category and location, view provider details, and submit reviews.

## 🚀 Key Features

### Customer Features
- Browse approved service providers.
- Search providers by name and keywords.
- Filter providers by category, governorate, and area.
- Navigate through paginated search results.
- View provider profiles and service details.
- View ratings and customer reviews.
- Open provider locations using Google Maps.
- Register and log in securely.

### Service Provider Features
- Register as a service provider.
- Submit service details for admin approval.
- Track application approval status.
- Manage provider information.

### Admin Features
- Review pending service provider applications.
- Approve or reject provider requests.
- Manage service categories.

## 🛠️ Tech Stack

### Backend
- C#
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication & Role-Based Authorization
- Repository Pattern
- Unit of Work Pattern
- Specification Pattern
- Data Transfer Objects (DTOs)
- Dependency Injection
- Swagger / OpenAPI

### Frontend
- Angular 17
- TypeScript
- HTML5
- SCSS
- RxJS
- Angular Router
- HTTP Interceptors
- ngx-translate (Arabic & English)
- ngx-toastr

## 🏗️ Project Architecture

The backend follows a layered architecture:

```text
Fltrni
│
├── Fltrni
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── Fltrni.core
│   ├── DTO
│   ├── Entities
│   ├── Interface
│   ├── Specifications
│   └── Exceptions
│
└── Fltrni.infrastructure
    ├── Data
    ├── Migrations
    ├── Repository
    └── Services

angular-Filtrni
└── src
    ├── app
    ├── assets
    └── styles.scss
```

### Architecture Responsibilities

- **Core:** Contains business entities, DTOs, interfaces, specifications, and application contracts.
- **Infrastructure:** Handles database access, Entity Framework Core, repositories, and service implementations.
- **API:** Exposes RESTful endpoints through controllers and configures authentication, authorization, and dependency injection.
- **Angular:** Provides the user interface and communicates with the backend API.

## ⚙️ Getting Started

### Prerequisites

- .NET 8 SDK
- Node.js and npm
- SQL Server
- Angular CLI 17

### 1. Clone the Repository

```bash
git clone https://github.com/Hossam2027/Fltrni.git
cd Fltrni
```

### 2. Backend Configuration

Configure your local database connection string and JWT settings in `appsettings.Development.json` inside the API project.

Example:

```json
{
  "ConnectionStrings": {
    "CS": "YOUR_SQL_SERVER_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "YOUR_LONG_RANDOM_SECRET_KEY",
    "Issuer": "Fltrni",
    "Audience": "Fltrni"
  }
}
```

Never commit real passwords, connection strings, or JWT secrets.

### 3. Apply Database Migrations

From the repository root:

```bash
dotnet ef database update --project ./Fltrni/Fltrni.infrastructure --startup-project ./Fltrni/Fltrni
```

### 4. Run the Backend

```bash
cd Fltrni/Fltrni
dotnet restore
dotnet run
```

Swagger is enabled in the Development environment.

### 5. Run the Frontend

Open another terminal:

```bash
cd angular-Filtrni
npm install
npm start
```

Update the frontend API base URL in its environment configuration to match your backend address.

The Angular development server runs at:

```text
http://localhost:4200
```

## 🌍 Localization

The application supports:
- Arabic
- English

Users can switch between the supported languages.

## 🔐 Authentication & Authorization

- User authentication with ASP.NET Core Identity.
- JWT-based authentication.
- Role-based authorization.
- Supported roles: Admin, Provider, and Customer.
- Protected API endpoints.

## 👨‍💻 Author

**Hossam Mohamed**

Full Stack .NET Developer

- GitHub: [Hossam2027](https://github.com/Hossam2027)

---

⭐ If you find this project interesting, feel free to explore the repository!