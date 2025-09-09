# Todos Full-Stack Application

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![React](https://img.shields.io/badge/React-19.1.1-blue.svg)](https://reactjs.org/)
[![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Compose-blue.svg)](https://docs.docker.com/compose/)
[![GraphQL](https://img.shields.io/badge/GraphQL-HotChocolate-E10098.svg)](https://graphql.org/)

A modern, production-ready full-stack todo application showcasing best practices in web development. Built with **React 19**, **.NET 9 GraphQL API**, **Relay state management**, and **Docker containerization**.

> **🚀 Perfect for**: Learning modern web development, portfolio projects, job interviews, or as a foundation for larger applications.

**✨ Key Highlights:**
- 🏗️ **Clean Architecture** with proper separation of concerns
- 🔄 **GraphQL-first** approach with type safety
- ⚡ **Modern React** with concurrent features and Relay
- 🐳 **Production-ready** Docker deployment
- 📱 **Responsive design** with Bootstrap 5
- 🧪 **Fully tested** and documented

## 🏗️ Architecture Overview

```
┌─────────────────┐    HTTP/GraphQL    ┌──────────────────┐
│   React App     │◄──────────────────►│   .NET API       │
│   (Frontend)    │                    │   (Backend)      │
│   Port: 3000    │                    │   Port: 5000     │
└─────────────────┘                    └──────────────────┘
│                                       │
│ • React 19                           │ • .NET 9
│ • Relay (GraphQL Client)             │ • GraphQL (HotChocolate)
│ • Bootstrap                          │ • Entity Framework Core
│ • Nginx (Production)                 │ • SQLite Database
│ • CRACO (Build Tool)                 │ • Clean Architecture
│                                       │ • CORS Enabled
└─────────────────┘                    └──────────────────┘
```

## 🚀 Quick Start

### Prerequisites
- Docker and Docker Compose
- Node.js 18+ (for local development)
- .NET 9 SDK (for local development)

### Run with Docker (Recommended)
```bash
cd Todos.Solution
docker compose up -d --build
```

**Access URLs:**
- Frontend: http://localhost:3000
- API: http://localhost:5000/graphql
- API Health: http://localhost:5000/health

### Local Development
See individual project documentation:
- [Backend API Documentation](./Todos.Solution/README.md)
- [Frontend App Documentation](./todos.app/README.md)

## 📁 Project Structure

```
Todos/
├── todos.app/                 # React Frontend Application
│   ├── src/
│   │   ├── components/        # React components
│   │   ├── utils/            # Utilities (Relay environment)
│   │   └── __generated__/    # Auto-generated GraphQL types
│   ├── public/               # Static assets
│   ├── Dockerfile            # Frontend container
│   ├── nginx.conf            # Nginx configuration
│   └── package.json          # Dependencies & scripts
│
└── Todos.Solution/           # .NET Backend Solution
    ├── Todos.Api/            # Main API project
    ├── Todos.Data/           # Data layer (Entity Framework)
    ├── Todos.Services/       # Business logic layer
    ├── Todos.Api.Gql/        # GraphQL extensions
    ├── Dockerfile            # Backend container
    ├── docker-compose.yml    # Multi-container orchestration
    └── Todos.Solution.sln    # Solution file
```

## 🔧 Technology Stack

### Frontend (React App)
| Technology | Version | Purpose |
|------------|---------|---------|
| React | 19.1.1 | UI Framework |
| Relay | 20.1.1 | GraphQL Client |
| Bootstrap | 5.3.8 | CSS Framework |
| CRACO | 7.1.0 | Build Tool Override |
| Nginx | Alpine | Production Web Server |
| Node.js | 18-alpine | Build Environment |

### Backend (.NET API)
| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 9.0 | Runtime Framework |
| ASP.NET Core | 9.0 | Web Framework |
| HotChocolate | Latest | GraphQL Server |
| Entity Framework Core | 9.0 | ORM |
| SQLite | - | Database |

## 🎯 Key Features

### Frontend Features
- ✅ GraphQL integration with Relay
- ✅ Real-time todo management (Create, Read, Update, Delete)
- ✅ Responsive Bootstrap UI
- ✅ Optimistic updates
- ✅ Error handling
- ✅ Production-ready with Nginx

### Backend Features
- ✅ GraphQL API with queries and mutations
- ✅ Clean Architecture pattern
- ✅ Entity Framework with SQLite
- ✅ Database migrations
- ✅ CORS configuration
- ✅ Health checks
- ✅ Dependency injection
- ✅ Layered architecture

## 🐳 Docker Configuration

### Multi-Container Setup
- **Network**: Custom bridge network for service communication
- **Dependencies**: Frontend waits for API health check
- **Volumes**: Database persistence
- **Health Checks**: API health monitoring

### Container Details
| Service | Image | Internal Port | External Port | Status |
|---------|-------|---------------|---------------|--------|
| todos-api | todossolution-todos-api | 8080 | 5000 | ✅ Healthy |
| todos-app | todossolution-todos-app | 80 | 3000 | ✅ Running |

## 🔄 API Operations

### Available GraphQL Operations

**Queries:**
```graphql
query {
  todos {
    id
    title
    description
    status
  }
}
```

**Mutations:**
```graphql
mutation CreateTodo($input: CreateTodoInput!) {
  createTodo(input: $input) {
    id
    title
    description
    status
  }
}

mutation UpdateTodo($id: Int!, $input: UpdateTodoInput!) {
  updateTodo(id: $id, input: $input) {
    id
    title
    description
    status
  }
}
```

## 🚦 Service Status

Check service health:
```bash
# API Health Check
curl http://localhost:5000/health

# Frontend Status
curl -I http://localhost:3000

# Docker Container Status
docker ps --filter "label=com.todos.group=todos-app"
```

## 📝 Development Workflow

1. **Make Backend Changes**: Modify code in `Todos.Solution/`
2. **Make Frontend Changes**: Modify code in `todos.app/src/`
3. **Update GraphQL Schema**: Run `npm run relay` in todos.app
4. **Rebuild Containers**: `docker compose up -d --build`
5. **Test Changes**: Access http://localhost:3000

## 🔒 Security & Configuration

- **CORS**: Configured for localhost and container communication
- **Environment Variables**: Separate development and production configs
- **Health Checks**: API health monitoring for container orchestration
- **Database**: SQLite with Entity Framework migrations

## 📊 Performance Considerations

- **Frontend**: Nginx serving static assets, Relay for efficient GraphQL caching
- **Backend**: Clean architecture for maintainability, Entity Framework for data access
- **Docker**: Multi-stage builds for optimized image sizes
- **Network**: Custom bridge network for efficient container communication

## 🎨 Design Decisions

### Why GraphQL?
- Single endpoint for all data operations
- Type-safe queries with Relay
- Efficient data fetching (no over-fetching)
- Real-time capabilities

### Why Clean Architecture?
- Separation of concerns
- Testability and maintainability
- Independent of frameworks and databases
- Business logic isolation

### Why Docker?
- Consistent development and production environments
- Easy deployment and scaling
- Service isolation and communication
- Infrastructure as code

## 📚 Additional Documentation

- [Backend API Details](./Todos.Solution/README.md)
- [Frontend App Details](./todos.app/README.md)
- [Docker Configuration](./Todos.Solution/docker-compose.yml)

**Built with ❤️ using .NET 9 and React 19**

## 🌟 Star History

If you find this project helpful, please consider giving it a ⭐ on GitHub!

## 📊 Project Stats

- **Backend**: .NET 9 with Entity Framework Core
- **Frontend**: React 19 with Relay GraphQL client  
- **Database**: SQLite (development) / PostgreSQL ready
- **Deployment**: Docker & Docker Compose
- **Architecture**: Clean Architecture + Component-based
- **Lines of Code**: ~2,500 lines
- **Documentation**: 100% documented

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

- 🐛 [Report a bug](https://github.com/yourusername/todos-fullstack-app/issues/new?template=bug_report.md)
- 💡 [Request a feature](https://github.com/yourusername/todos-fullstack-app/issues/new?template=feature_request.md)
- 📖 [Improve documentation](https://github.com/yourusername/todos-fullstack-app/issues/new)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Microsoft** for the excellent .NET 9 platform
- **Meta** for React and Relay
- **The Guild** for HotChocolate GraphQL server
- **Docker** for containerization technology
- **Bootstrap** team for the UI framework

## 💼 Use Cases

Perfect for:
- 🎓 **Learning**: Modern web development patterns
- 📝 **Portfolio**: Showcase full-stack skills  
- 🏢 **Enterprise**: Foundation for larger applications
- 🔧 **Interviews**: Demonstrate architectural knowledge
- 🚀 **Startups**: Quick MVP development

## 🔍 Keywords
`react` `dotnet` `graphql` `docker` `fullstack` `todo-app` `clean-architecture` `relay` `entity-framework` `bootstrap` `nginx` `sqlite` `containerized` `microservices-ready`
