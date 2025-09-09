# Technical Architecture - Todos Application

Comprehensive technical architecture documentation for the full-stack Todos application.

## 🏗️ System Architecture Overview

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                           Client Layer (Browser)                                │
│  ┌─────────────────────────────────────────────────────────────────────────────┐ │
│  │                     React Application (todos-app)                           │ │
│  │  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐  ┌─────────────────┐   │ │
│  │  │ Components  │  │   Relay     │  │ Bootstrap   │  │ Environment     │   │ │
│  │  │   (JSX)     │  │   Store     │  │    CSS      │  │   Variables     │   │ │
│  │  └─────────────┘  └─────────────┘  └─────────────┘  └─────────────────┘   │ │
│  └─────────────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────────────────────┘
                          │ HTTP/GraphQL over Docker Network
                          │ (Container-to-Container Communication)
┌─────────────────────────▼───────────────────────────────────────────────────────┐
│                          Server Layer (.NET)                                    │
│  ┌─────────────────────────────────────────────────────────────────────────────┐ │
│  │                    .NET API Application (todos-api)                         │ │
│  │                                                                             │ │
│  │  ┌─────────────────────────────────────────────────────────────────────────┐ │ │
│  │  │                     Presentation Layer                                 │ │ │
│  │  │           (Todos.Api - ASP.NET Core Web API)                           │ │ │
│  │  │  • Program.cs (Startup Configuration)                                  │ │ │
│  │  │  • CORS Policy & Middleware Pipeline                                   │ │ │
│  │  │  • Health Checks & Monitoring                                          │ │ │
│  │  │  • Dependency Injection Container                                      │ │ │
│  │  └─────────────────────────────────────────────────────────────────────────┘ │ │
│  │                                    │                                         │ │
│  │  ┌─────────────────────────────────▼─────────────────────────────────────────┐ │ │
│  │  │                         GraphQL Layer                                   │ │ │
│  │  │              (Todos.Api.Gql - HotChocolate)                             │ │ │
│  │  │  • Query Extensions & Resolvers                                         │ │ │
│  │  │  • Mutation Extensions & Input Types                                    │ │ │
│  │  │  • GraphQL Schema Definition                                            │ │ │
│  │  │  • Type System Configuration                                            │ │ │
│  │  └─────────────────────────────────▼─────────────────────────────────────────┘ │ │
│  │                                    │                                         │ │
│  │  ┌─────────────────────────────────▼─────────────────────────────────────────┐ │ │
│  │  │                       Application Layer                                 │ │ │
│  │  │                  (Todos.Services - Business Logic)                      │ │ │
│  │  │  • ITodoService Interface                                               │ │ │
│  │  │  • TodoService Implementation                                           │ │ │
│  │  │  • DTOs (CreateTodoInput, UpdateTodoInput)                              │ │ │
│  │  │  • Domain Models (TodoModel)                                            │ │ │
│  │  │  • Business Rules & Validation                                          │ │ │
│  │  └─────────────────────────────────▼─────────────────────────────────────────┘ │ │
│  │                                    │                                         │ │
│  │  ┌─────────────────────────────────▼─────────────────────────────────────────┐ │ │
│  │  │                      Infrastructure Layer                               │ │ │
│  │  │               (Todos.Data - Entity Framework Core)                      │ │ │
│  │  │  • TodosDbContext (Database Context)                                    │ │ │
│  │  │  • Todo Entity Model                                                    │ │ │
│  │  │  • Database Migrations                                                  │ │ │
│  │  │  • SQLite Database Configuration                                        │ │ │
│  │  └─────────────────────────────────▼─────────────────────────────────────────┘ │ │
│  └─────────────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────────────────────┘
                          │
┌─────────────────────────▼───────────────────────────────────────────────────────┐
│                         Data Layer                                              │
│  ┌─────────────────────────────────────────────────────────────────────────────┐ │
│  │                     SQLite Database                                         │ │
│  │  • File-based storage (Todos.db)                                           │ │
│  │  • ACID compliance                                                          │ │
│  │  • Zero-configuration setup                                                │ │
│  │  • Persistent volume in Docker                                             │ │
│  └─────────────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────────────────┘
```

## 🎯 Design Principles & Patterns

### 1. Clean Architecture (Backend)
**Philosophy**: Dependency inversion with business logic at the center

**Layer Dependencies**:
```
Presentation → Application → Infrastructure
     ↓              ↓            ↓
   API Layer → Services → Data Access
```

**Benefits**:
- ✅ Testable business logic
- ✅ Framework independence  
- ✅ Database independence
- ✅ UI independence
- ✅ Maintainable codebase

### 2. Component-Based Architecture (Frontend)
**Philosophy**: Composable, reusable UI components

```javascript
// Component Hierarchy
App
├── ErrorBoundary
├── TodoList
│   ├── TodoItem
│   ├── AddTodoForm
│   └── FilterControls
└── LoadingSpinner
```

### 3. GraphQL-First API Design
**Philosophy**: Single endpoint, strong typing, efficient data fetching

**Schema-Driven Development**:
1. Define GraphQL schema
2. Generate TypeScript types
3. Implement resolvers
4. Build UI components

## 🔧 Technology Decision Matrix

### Frontend Technology Choices

| Decision | Options Considered | Choice | Rationale |
|----------|-------------------|--------|-----------|
| **UI Framework** | React, Vue, Angular, Svelte | React 19 | Mature ecosystem, concurrent features, wide adoption |
| **State Management** | Redux, Zustand, Context API, Relay | Relay | GraphQL-first, automatic normalization, type safety |
| **CSS Framework** | Tailwind, Material-UI, Ant Design | Bootstrap 5 | Rapid prototyping, consistent design, responsive grid |
| **Build Tool** | Vite, Webpack, Parcel | Create React App + CRACO | Stability, community support, Babel integration |
| **GraphQL Client** | Apollo Client, urql, graphql-request | Relay | Optimized for performance, Facebook's production choice |

### Backend Technology Choices

| Decision | Options Considered | Choice | Rationale |
|----------|-------------------|--------|-----------|
| **Runtime** | .NET 6, .NET 7, Node.js, Java | .NET 9 | Latest features, performance, long-term support |
| **API Pattern** | REST, GraphQL, gRPC | GraphQL | Type safety, single endpoint, efficient data fetching |
| **GraphQL Server** | Apollo Server, HotChocolate, GraphQL.NET | HotChocolate | Native .NET integration, performance, code-first |
| **ORM** | Entity Framework, Dapper, ADO.NET | EF Core | Code-first, migrations, LINQ support |
| **Database** | PostgreSQL, SQL Server, SQLite, MongoDB | SQLite | Zero config, file-based, perfect for demos |

### Infrastructure Technology Choices

| Decision | Options Considered | Choice | Rationale |
|----------|-------------------|--------|-----------|
| **Containerization** | Docker, Podman, containerd | Docker | Industry standard, excellent tooling, Docker Compose |
| **Web Server** | Apache, IIS, Nginx, Caddy | Nginx | Lightweight, excellent static file serving, reverse proxy |
| **Orchestration** | Kubernetes, Docker Swarm, Docker Compose | Docker Compose | Simple multi-container apps, local development |

## 🌐 Communication Patterns

### 1. Client-Server Communication
```
┌─────────────┐    HTTP POST /graphql    ┌─────────────┐
│   Browser   │ ◄────────────────────────► │  API Server │
│ (React App) │                           │ (.NET Core) │
└─────────────┘                           └─────────────┘
     │                                           │
     │ GraphQL Query/Mutation                    │
     │ Content-Type: application/json            │
     │                                           │
     ▼                                           ▼
┌─────────────┐                           ┌─────────────┐
│ Relay Store │                           │ HotChocolate│
│   Cache     │                           │   Schema    │
└─────────────┘                           └─────────────┘
```

### 2. Internal Service Communication
```
┌─────────────────────────────────────────────────────────────┐
│                    .NET Application                         │
│                                                             │
│  GraphQL Layer    ─────────────►   Application Layer       │
│  (Resolvers)                        (Services)             │
│       │                                  │                  │
│       │                                  ▼                  │
│       │                          Infrastructure Layer      │
│       │                           (Data Access)            │
│       │                                  │                  │
│       └──────────── Response ◄───────────┘                  │
└─────────────────────────────────────────────────────────────┘
```

### 3. Container Communication
```
┌─────────────────┐     Docker Network     ┌─────────────────┐
│   todos-app     │ ◄──────────────────────► │   todos-api     │
│   (port 80)     │   http://todos-api:8080  │   (port 8080)   │
│   Nginx         │                          │   Kestrel       │
└─────────────────┘                          └─────────────────┘
        │                                            │
        │ External Access                            │ External Access
        ▼                                            ▼
┌─────────────────┐                          ┌─────────────────┐
│  localhost:3000 │                          │  localhost:5000 │
└─────────────────┘                          └─────────────────┘
```

## 🔄 Data Flow Architecture

### 1. Query Data Flow (Read Operations)
```
User Interaction → React Component → Relay Query → GraphQL Server → Service Layer → EF Core → SQLite
                                                                                             │
Response ← UI Update ← Relay Cache ← GraphQL Response ← Business Logic ← Data Access ← ──┘
```

### 2. Mutation Data Flow (Write Operations)
```
User Action → Form Submit → Relay Mutation → GraphQL Mutation → Service Layer → EF Core → SQLite
                    │                                                                       │
            Optimistic Update                                                               │
                    │                                                                       │
                    ▼                                                                       │
              UI Updates ← ─ ─ ─ ─ ─ ─ ─ ─ Server Response ← ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘
              (Confirmed)
```

### 3. Error Handling Flow
```
Error Source → Service Layer → GraphQL Error → Relay Error → React Error Boundary → User Feedback
     │              │              │              │                    │
     ▼              ▼              ▼              ▼                    ▼
  Database      Business       Schema        Network             UI Error
   Error         Rule         Validation     Error              Display
```

## 🔒 Security Architecture

### 1. Defense in Depth Strategy
```
┌─────────────────────────────────────────────────────────────────┐
│                     Browser Security                            │
│  • Content Security Policy (CSP)                               │
│  • HTTPS Enforcement                                            │
│  • XSS Protection Headers                                       │
└─────────────────────────┬───────────────────────────────────────┘
                          │
┌─────────────────────────▼───────────────────────────────────────┐
│                     Network Security                            │
│  • CORS Configuration                                           │
│  • Docker Network Isolation                                     │
│  • Rate Limiting (Future)                                       │
└─────────────────────────┬───────────────────────────────────────┘
                          │
┌─────────────────────────▼───────────────────────────────────────┐
│                  Application Security                           │
│  • Input Validation (GraphQL Schema)                            │
│  • Business Rule Enforcement                                    │
│  • SQL Injection Prevention (EF Core)                           │
└─────────────────────────┬───────────────────────────────────────┘
                          │
┌─────────────────────────▼───────────────────────────────────────┐
│                     Data Security                               │
│  • Database Access Control                                      │
│  • Connection String Security                                   │
│  • File System Permissions                                      │
└─────────────────────────────────────────────────────────────────┘
```

### 2. CORS Configuration Strategy
```csharp
// Development
"AllowedOrigins": ["http://localhost:3000"]

// Production  
"AllowedOrigins": [
    "http://todos-app",      // Docker internal
    "http://todos-app:80",   // Docker with port
    "https://yourdomain.com" // External domain
]
```

## 📊 Performance Architecture

### 1. Frontend Performance Strategy
```
┌─────────────────────────────────────────────────────────────────┐
│                    Client-Side Performance                      │
│                                                                 │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────────┐  │
│  │   Relay     │  │  Code       │  │     Bundle              │  │
│  │ Normalization│  │ Splitting   │  │   Optimization          │  │
│  │ & Caching   │  │ (Lazy Load) │  │  (Webpack/CRA)          │  │
│  └─────────────┘  └─────────────┘  └─────────────────────────┘  │
│                                                                 │
│  ┌─────────────────────────────────────────────────────────────┐  │
│  │                Browser Caching                             │  │
│  │  • Static Assets (CSS, JS, Images)                         │  │
│  │  • API Response Caching (Relay)                            │  │
│  │  • Service Worker (Future PWA)                             │  │
│  └─────────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

### 2. Backend Performance Strategy
```
┌─────────────────────────────────────────────────────────────────┐
│                   Server-Side Performance                       │
│                                                                 │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────────┐  │
│  │  GraphQL    │  │   EF Core   │  │      Database           │  │
│  │ Query       │  │ Query       │  │    Optimization         │  │
│  │ Optimization│  │ Optimization│  │   (Indexes, etc.)       │  │
│  └─────────────┘  └─────────────┘  └─────────────────────────┘  │
│                                                                 │
│  ┌─────────────────────────────────────────────────────────────┐  │
│  │              Connection & Resource Management               │  │
│  │  • Connection Pooling (EF Core)                            │  │
│  │  • Dependency Injection (Singleton Services)               │  │
│  │  • Async/Await Pattern                                     │  │
│  └─────────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

### 3. Infrastructure Performance
```
┌─────────────────────────────────────────────────────────────────┐
│                Docker & Deployment Performance                  │
│                                                                 │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────────┐  │
│  │Multi-Stage  │  │   Image     │  │    Container            │  │
│  │   Build     │  │Optimization │  │   Resource Limits       │  │
│  │             │  │(Alpine Base)│  │   (CPU, Memory)         │  │
│  └─────────────┘  └─────────────┘  └─────────────────────────┘  │
│                                                                 │
│  ┌─────────────────────────────────────────────────────────────┐  │
│  │                  Network Optimization                      │  │
│  │  • Docker Bridge Network                                   │  │
│  │  • Service Discovery (Container Names)                     │  │
│  │  • Health Checks for Availability                          │  │
│  └─────────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

## 🔄 Scalability Considerations

### 1. Current Architecture Limitations
| Component | Current Limit | Bottleneck | Solution Path |
|-----------|---------------|------------|---------------|
| Database | Single SQLite file | Concurrent writes | Move to PostgreSQL |
| API Server | Single container | CPU/Memory bound | Horizontal scaling |
| Frontend | Single Nginx instance | Static file serving | CDN integration |
| Network | Docker bridge | Container communication | Load balancer |

### 2. Scaling Roadmap
```
Current State → Intermediate → Production Scale
     │               │              │
     ▼               ▼              ▼
┌─────────────┐ ┌─────────────┐ ┌─────────────┐
│Single       │ │Multi-       │ │Kubernetes   │
│Container    │ │Container    │ │Cluster      │
│Deploy       │ │with LB      │ │with Auto    │
│             │ │             │ │Scaling      │
└─────────────┘ └─────────────┘ └─────────────┘
│SQLite       │ │PostgreSQL   │ │Managed      │
│Database     │ │Single       │ │Database     │
│             │ │Instance     │ │(RDS/Azure)  │
└─────────────┘ └─────────────┘ └─────────────┘
```

## 🎯 Future Architecture Enhancements

### 1. Microservices Evolution
```
Current Monolith → Domain-Driven Services
┌─────────────────┐   ┌─────────────────┐ ┌─────────────────┐
│   Todos API     │   │  User Service   │ │ Notification    │
│   (All Logic)   │ → │                 │+│   Service       │
│                 │   │                 │ │                 │
└─────────────────┘   └─────────────────┘ └─────────────────┘
```

### 2. Advanced Features Integration
- **Real-time Updates**: GraphQL subscriptions with SignalR
- **Offline Support**: Service Worker + IndexedDB cache
- **Authentication**: JWT tokens + OAuth2 integration  
- **Monitoring**: Application Insights + Prometheus metrics
- **Caching**: Redis for distributed caching
- **Search**: Elasticsearch for advanced search capabilities

### 3. Infrastructure Maturation
```
Development → Staging → Production
     │            │          │
     ▼            ▼          ▼
┌─────────────┐ ┌──────────┐ ┌─────────────────┐
│Docker       │ │Docker    │ │Kubernetes       │
│Compose      │ │Swarm     │ │+ Helm Charts    │
│             │ │          │ │+ Service Mesh   │
└─────────────┘ └──────────┘ └─────────────────┘
```

---

## 📋 Architecture Decision Records (ADRs)

### ADR-001: GraphQL Over REST
**Status**: Accepted  
**Context**: API design pattern choice  
**Decision**: Use GraphQL for API layer  
**Consequences**: 
- ✅ Type safety and single endpoint
- ✅ Efficient data fetching
- ❌ Learning curve for team
- ❌ Complexity for simple CRUD

### ADR-002: Clean Architecture Pattern  
**Status**: Accepted  
**Context**: Backend architecture pattern  
**Decision**: Implement Clean Architecture with DI  
**Consequences**:
- ✅ Testable and maintainable code
- ✅ Framework independence
- ❌ Initial complexity
- ❌ More files and abstractions

### ADR-003: Docker for Deployment
**Status**: Accepted  
**Context**: Deployment and environment consistency  
**Decision**: Use Docker containers with Docker Compose  
**Consequences**:
- ✅ Environment consistency
- ✅ Easy deployment
- ❌ Container orchestration complexity
- ❌ Additional resource overhead

---

**This architecture provides a solid foundation for a production-ready application with clear separation of concerns, scalability options, and maintainability.**
