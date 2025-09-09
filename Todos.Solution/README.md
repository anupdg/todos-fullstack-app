# Todos API - .NET Backend

A robust GraphQL API built with .NET 9, implementing Clean Architecture principles for a Todo management system.

## 🏗️ Architecture & Design Decisions

### Clean Architecture Implementation

```
┌─────────────────────────────────────────────────────────────┐
│                    Todos.Api (Presentation Layer)           │
│  • GraphQL Schema Configuration                             │
│  • Dependency Injection Setup                               │
│  • CORS Configuration                                       │
│  • Health Checks                                            │
└─────────────────┬───────────────────────────────────────────┘
                  │
┌─────────────────▼───────────────────────────────────────────┐
│              Todos.Api.Gql (GraphQL Layer)                  │
│  • Query Extensions                                         │
│  • Mutation Extensions                                      │
│  • GraphQL Type Definitions                                 │
│  • Resolver Logic                                           │
└─────────────────┬───────────────────────────────────────────┘
                  │
┌─────────────────▼───────────────────────────────────────────┐
│             Todos.Services (Application Layer)              │
│  • Business Logic                                           │
│  • DTOs and Models                                          │
│  • Service Interfaces                                       │
│  • Validation Rules                                         │
└─────────────────┬───────────────────────────────────────────┘
                  │
┌─────────────────▼───────────────────────────────────────────┐
│              Todos.Data (Infrastructure Layer)              │
│  • Entity Framework DbContext                               │
│  • Entity Models                                            │
│  • Database Migrations                                      │
│  • Repository Pattern                                       │
└─────────────────────────────────────────────────────────────┘
```

## 🎯 Project Breakdown

### 1. Todos.Api (Main API Project)

**Responsibilities:**
- Application entry point and configuration
- Dependency injection container setup
- Middleware pipeline configuration
- CORS policy definition
- Health check endpoints

**Key Files:**
```
Todos.Api/src/
├── Program.cs                  # Application startup and configuration
├── appsettings.json            # Development configuration
├── appsettings.Production.json # Production configuration
├── Todos.Api.csproj            # Project dependencies
└── Todos.db                    # SQLite database file
```

**Configuration Highlights:**
```csharp
// CORS Configuration
services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Health Check Endpoint
app.MapGet("/health", () => 
    Microsoft.AspNetCore.Http.Results.Ok(new { 
        status = "healthy", 
        timestamp = DateTime.UtcNow 
    }));
```

### 2. Todos.Api.Gql (GraphQL Layer)

**Responsibilities:**
- GraphQL schema definition
- Query and mutation resolvers
- GraphQL extensions and configuration
- Type system setup

**Key Files:**
```
Todos.Api.Gql/
├── GqlServiceCollectionExtensions.cs  # DI extensions
├── IQueryExtension.cs                 # Query interface
├── IMutationExtension.cs              # Mutation interface
├── Constants.cs                       # GraphQL constants
└── Todos.Api.Gql.csproj               # Project dependencies
```

**Design Pattern:**
- Uses Extension-based GraphQL setup
- Implements Query and Mutation extensions
- Type-safe GraphQL resolvers

### 3. Todos.Services (Business Logic Layer)

**Responsibilities:**
- Core business logic implementation
- Data transfer objects (DTOs)
- Service contracts and implementations
- Input validation and business rules

**Key Components:**

**Service Interface:**
```csharp
public interface ITodoService
{
    Task<List<TodoModel>> GetTodosAsync();
    Task<TodoModel> CreateTodoAsync(CreateTodoInput input);
    Task<TodoModel> UpdateTodoAsync(Guid id, UpdateTodoInput input);
    Task<bool> DeleteTodoAsync(Guid id);
}
```

**Models & DTOs:**
```
Todos.Services/
├── Services/
│   └── TodoService.cs              # Business logic implementation
├── Interfaces/
│   └── ITodoService.cs             # Service contract
├── Models/
│   ├── TodoModel.cs                # Domain model
│   ├── CreateTodoInput.cs          # Create DTO
│   └── UpdateTodoInput.cs          # Update DTO
└── ServiceCollectionExtensions.cs  # DI registration
```

### 4. Todos.Data (Data Access Layer)

**Responsibilities:**
- Entity Framework Core configuration
- Database context management
- Entity model definitions
- Database migrations
- Data persistence logic

**Entity Model:**
```csharp
public class Todo
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Status { get; set; }
}
```

**Database Context:**
```csharp
public class TodosDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    
    // Configuration and seed data
}
```

## 🔧 Technology Choices & Rationale

### 1. GraphQL with HotChocolate
**Why GraphQL?**
- Single endpoint for all operations
- Type-safe queries and mutations  
- Efficient data fetching (no over/under-fetching)
- Built-in introspection and documentation
- Real-time subscriptions capability

**Why HotChocolate?**
- Native .NET GraphQL server
- Excellent performance
- Strong typing support
- Code-first approach
- Extension-based architecture

### 2. Entity Framework Core with SQLite
**Why EF Core?**
- Code-first approach with migrations
- LINQ query capabilities
- Change tracking and lazy loading
- Cross-database compatibility

**Why SQLite?**
- Zero configuration setup
- File-based database (easy deployment)
- ACID compliance
- Perfect for demo/development scenarios

### 3. Clean Architecture Pattern
**Benefits:**
- **Separation of Concerns**: Each layer has distinct responsibilities
- **Testability**: Business logic isolated from infrastructure
- **Maintainability**: Easy to modify and extend
- **Independence**: Framework and database agnostic core

**Dependency Direction:**
```
API → Services → Data
(Outer layers depend on inner layers)
```

## 🚀 Development Setup

### Prerequisites
```bash
# Required tools
.NET 9 SDK
Docker (optional)
SQLite (optional for direct DB access)
```

### Local Development
```bash
# Restore dependencies
dotnet restore

# Update database
dotnet ef database update -p Todos.Data/src -s Todos.Api/src

# Run the API
dotnet run -p Todos.Api/src

# Access GraphQL playground
http://localhost:5000/graphql
```

### Database Migrations
```bash
# Create new migration
dotnet ef migrations add <MigrationName> -p Todos.Data/src -s Todos.Api/src

# Update database
dotnet ef database update -p Todos.Data/src -s Todos.Api/src

# Remove last migration
dotnet ef migrations remove -p Todos.Data/src -s Todos.Api/src
```

## 📊 GraphQL Schema

### Available Operations

**Query:**
```graphql
type query {
  todos: [TodoModel] @cost(weight: "10")
  todo(id: UUID!): TodoModel @cost(weight: "10")
}
```

**Mutations:**
```graphql
type mutation {
  createTodo(input: CreateTodoInput): TodoModel @cost(weight: "10")
  updateTodo(id: UUID!, input: UpdateTodoInput): TodoModel @cost(weight: "10")
  deleteTodo(id: UUID!): Boolean! @cost(weight: "10")
}
```

**Types:**
```graphql
type TodoModel {
  id: UUID!
  title: String!
  description: String!
  isCompleted: Boolean!
  status: String!
}

input CreateTodoInput {
  title: String!
  description: String!
  status: String!
  isCompleted: Boolean!
}

input UpdateTodoInput {
  title: String!
  description: String!
  status: String!
  isCompleted: Boolean!
}

scalar UUID @specifiedBy(url: "https://tools.ietf.org/html/rfc4122")
```

## 🐳 Docker Configuration

### Dockerfile Highlights
```dockerfile
# Multi-stage build for optimized image size
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Install curl for health checks
RUN apt-get update && apt-get install -y curl

# Build and publish application
RUN dotnet publish "Todos.Api.csproj" -c Release -o /app/publish
```

### Environment Configuration
```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ASPNETCORE_URLS=http://+:8080
```

## 🔒 Security & Configuration

### CORS Policy
```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:3000",
      "http://localhost:3001",
      "http://todos-app",
      "http://todos-app:80"
    ]
  }
}
```

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Todos.db"
  }
}
```

## 🧪 Testing the API

### Health Check
```bash
curl http://localhost:5000/health
```

### GraphQL Query Example
```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{
    "query": "query { todos { id title description status isCompleted } }"
  }'
```

### Create Todo Mutation
```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{
    "query": "mutation($input: CreateTodoInput!) { createTodo(input: $input) { id title description status } }",
    "variables": {
      "input": {
        "title": "New Todo",
        "description": "Todo description",
        "status": "Pending"
      }
    }
  }'
```

## 📈 Performance Considerations

### Database
- **SQLite**: Lightweight, file-based database
- **EF Core**: Optimized queries with LINQ
- **Connection Pooling**: Automatic connection management

### GraphQL
- **Query Optimization**: HotChocolate's built-in optimizations
- **Type Safety**: Compile-time query validation
- **Caching**: Built-in query result caching capabilities

### Architecture
- **Layered Architecture**: Clean separation for better performance monitoring
- **Dependency Injection**: Singleton services for better memory usage
- **Async/Await**: Non-blocking I/O operations

## 🔄 CI/CD Considerations

### Docker Build Optimization
- Multi-stage builds for smaller images
- Layer caching for faster builds
- Health checks for deployment verification

### Environment Management
- Separate configuration files for different environments
- Environment variable overrides
- Secret management for production

---

**Next Steps:** Check out the [Frontend Documentation](../todos.app/README.md) to understand the complete application architecture.
