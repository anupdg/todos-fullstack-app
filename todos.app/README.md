# Todos App - React Frontend

A modern React application built with React 19, Relay, and Bootstrap, providing a responsive interface for todo management through GraphQL.

## 🏗️ Architecture & Design Decisions

### Component Architecture

```
┌───────────────────────────────────────────────────────────────┐
│                     Browser (Client)                          │
│  ┌─────────────────────────────────────────────────────────┐  │
│  │                React Application                        │  │
│  │  ┌─────────────────┐  ┌─────────────────┐  ┌─────────┐  │  │
│  │  │   Components    │  │   Relay Store   │  │  Utils  │  │  │
│  │  │   (UI Layer)    │  │  (State Mgmt)   │  │ (Relay) │  │  │
│  │  └─────────────────┘  └─────────────────┘  └─────────┘  │  │
│  └─────────────────────────────────────────────────────────┘  │
└─────────────────────┬─────────────────────────────────────────┘
                      │ HTTP/GraphQL
                      │
┌─────────────────────▼────────────────────────────────────────┐
│                   Docker Network                             │
│  ┌─────────────────────────────────────────────────────────┐ │
│  │              todos-api:8080                             │ │
│  │           (.NET GraphQL API)                            │ │
│  └─────────────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────────┘
```

## 🎯 Technology Stack & Rationale

### Core Technologies

| Technology | Version | Purpose | Why Chosen |
|------------|---------|---------|------------|
| **React** | 19.1.1 | UI Framework | Latest features, concurrent rendering, improved performance |
| **Relay** | 20.1.1 | GraphQL Client | Type-safe GraphQL, automatic query optimization, caching |
| **Bootstrap** | 5.3.8 | CSS Framework | Rapid responsive design, consistent UI components |
| **CRACO** | 7.1.0 | Build Override | Custom Babel configuration for Relay transforms |
| **Node.js** | 18-alpine | Runtime | LTS version for stability, alpine for smaller images |
| **Nginx** | alpine | Web Server | Production-ready static file serving, reverse proxy |

### Build Tools & Configuration

| Tool | Purpose | Configuration |
|------|---------|---------------|
| **Babel** | JS Transpilation | Relay plugin for GraphQL transforms |
| **Webpack** | Module Bundling | Via React Scripts (Create React App) |
| **ESLint** | Code Linting | React recommended rules |
| **Docker** | Containerization | Multi-stage build for optimization |

## 📁 Project Structure & Organization

```
todos.app/
├── public/                             # Static assets
│   ├── index.html                      # Main HTML template
│   ├── favicon.ico                     # App icon
│   └── manifest.json                   # PWA manifest
│
├── src/                                # Source code
│   ├── components/                     # React components
│   │   └── TodoList.js                 # Main todo list component
│   ├── utils/                          # Utility modules
│   │   └── RelayEnvironment.js         # Relay configuration
│   ├── __generated__/                  # Auto-generated GraphQL types
│   │   ├── TodosQuery.graphql.js
│   │   ├── AddTodoMutation.graphql.js
│   │   └── UpdateTodoMutation.graphql.js
│   ├── App.js                          # Main application component
│   ├── App.css                         # Global styles
│   ├── index.js                        # Application entry point
│   └── index.css                       # Base styles
│
├── .env.development                    # Development environment vars
├── .env.production                     # Production environment vars
├── .babelrc                            # Babel configuration
├── craco.config.js                     # CRACO configuration
├── relay.config.js                     # Relay compiler configuration
├── schema.graphql                      # GraphQL schema definition
├── package.json                        # Dependencies and scripts
├── Dockerfile                          # Container configuration
├── nginx.conf                          # Nginx server configuration
└── .dockerignore                       # Docker build exclusions
```

## 🔧 Key Configuration Files

### 1. Relay Configuration (`relay.config.js`)
```javascript
module.exports = {
  src: './src',
  language: 'javascript',
  schema: './schema.graphql',
  exclude: ['**/node_modules/**', '**/__mocks__/**', '**/__generated__/**'],
}
```

**Purpose:**
- Defines source directory for GraphQL queries
- Points to GraphQL schema file
- Configures code generation settings

### 2. CRACO Configuration (`craco.config.js`)
```javascript
module.exports = {
  babel: {
    plugins: ['relay'],
  },
};
```

**Purpose:**
- Enables Relay Babel plugin
- Transforms GraphQL template literals at build time
- Required for Relay to work with Create React App

### 3. Babel Configuration (`.babelrc`)
```javascript
{
  "presets": ["react-app"],
  "plugins": ["relay"]
}
```

**Purpose:**
- React app preset for JSX transformation
- Relay plugin for GraphQL query compilation

### 4. Environment Variables
**Development (`.env.development`):**
```
REACT_APP_API_URL=http://localhost:5000
```

**Production (`.env.production`):**
```
REACT_APP_API_URL=http://todos-api:8080
```

**Purpose:**
- Different API endpoints for different environments
- Development uses host URL, production uses Docker service name

## 🌐 Relay Integration & GraphQL

### Relay Environment Setup
```javascript
// utils/RelayEnvironment.js
import { Environment, Network, RecordSource, Store } from 'relay-runtime';

function fetchQuery(operation, variables) {
  const apiUrl = process.env.REACT_APP_API_URL || 'http://localhost:5000';
  return fetch(`${apiUrl}/graphql`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      query: operation.text,
      variables,
    }),
  }).then(response => response.json());
}

const environment = new Environment({
  network: Network.create(fetchQuery),
  store: new Store(new RecordSource()),
});
```

### GraphQL Operations

**Query Example:**
```javascript
import { graphql } from 'react-relay';

const TodosQuery = graphql`
  query TodosQuery {
    todos {
      id
      title
      description
      status
      isCompleted
    }
  }
`;
```

**Mutation Example:**
```javascript
const AddTodoMutation = graphql`
  mutation AddTodoMutation($input: CreateTodoInput!) {
    createTodo(input: $input) {
      id
      title
      description
      status
      isCompleted
    }
  }
`;
```

### Why Relay Over Apollo?

| Aspect | Relay | Apollo | Our Choice |
|--------|-------|---------|------------|
| **Performance** | Optimized queries, automatic batching | Good performance | ✅ Relay |
| **Type Safety** | Strong TypeScript integration | Good TS support | ✅ Relay |
| **Bundle Size** | Smaller runtime | Larger bundle | ✅ Relay |
| **Caching** | Normalized cache, automatic updates | Manual cache management | ✅ Relay |
| **Learning Curve** | Steeper | Gentler | Trade-off accepted |

## 🎨 UI/UX Design Decisions

### Bootstrap Integration
```javascript
// Using Bootstrap classes for responsive design
<div className="container-fluid">
  <div className="row">
    <div className="col-md-8 offset-md-2">
      <div className="card">
        <div className="card-body">
          {/* Todo content */}
        </div>
      </div>
    </div>
  </div>
</div>
```

### Responsive Design Strategy
- **Mobile First**: Bootstrap's mobile-first approach
- **Grid System**: 12-column responsive grid
- **Breakpoints**: SM (576px), MD (768px), LG (992px), XL (1200px)
- **Components**: Cards, forms, buttons for consistent UI

### User Experience Features
- **Optimistic Updates**: UI updates before server confirmation
- **Loading States**: Visual feedback during API calls
- **Error Handling**: User-friendly error messages
- **Form Validation**: Client-side validation for better UX

## 🐳 Docker Configuration & Production

### Multi-Stage Dockerfile
```dockerfile
# Build Stage
FROM node:18-alpine as build
WORKDIR /app
COPY package*.json ./
RUN npm ci                      # Clean install for production
COPY . .
RUN npm run build               # Build optimized production bundle

# Production Stage  
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

### Nginx Configuration
```nginx
server {
    listen 80;
    server_name localhost;
    
    # Serve React app
    location / {
        root /usr/share/nginx/html;
        index index.html index.htm;
        try_files $uri $uri/ /index.html;  # SPA routing
    }
    
    # Optional API proxy for development
    location /api/ {
        proxy_pass http://todos-api:8080/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

**Key Features:**
- **SPA Routing**: `try_files` directive handles client-side routing
- **Static Asset Serving**: Optimized for React build output
- **Proxy Support**: Optional API proxying for development
- **Alpine Base**: Minimal image size for production

## 🚀 Development Workflow

### Local Development Setup
```bash
# Install dependencies
npm install

# Start development server
npm start

# Run Relay compiler (generate GraphQL types)
npm run relay

# Build for production
npm run build

# Run tests
npm test
```

### GraphQL Schema Synchronization
```bash
# When backend schema changes:
1. Update schema.graphql file
2. Run: npm run relay
3. Update React components if needed
4. Test GraphQL operations
```

### Environment-Specific Development
```bash
# Development (uses localhost:5000)
npm start

# Production build (uses Docker service names)
npm run build
REACT_APP_API_URL=http://todos-api:8080 npm run build
```

## 🔄 State Management Strategy

### Relay Store Architecture
```
┌──────────────────────────────────────┐
│           Relay Store                │
│  ┌─────────────────────────────────┐ │
│  │      Record Source              │ │
│  │   (Normalized Cache)            │ │
│  │                                 │ │
│  │  Todo:1 → {id: 1, title: "..."} │ │
│  │  Todo:2 → {id: 2, title: "..."} │ │
│  │  ROOT    → {todos: [Todo:1, ...]} │ 
│  └─────────────────────────────────┘ │
│                                      │
│  ┌─────────────────────────────────┐ │
│  │       Network Layer             │ │
│  │   (GraphQL Queries)             │ │
│  └─────────────────────────────────┘ │
└──────────────────────────────────────┘
```

### Why Relay for State Management?

**Advantages:**
- **Automatic Normalization**: No manual cache management
- **Optimistic Updates**: UI updates before server response
- **Query Coalescing**: Automatic batching of queries
- **Cache Invalidation**: Smart cache updates on mutations
- **Type Safety**: Generated TypeScript types

**Trade-offs:**
- **Learning Curve**: More complex than useState/useReducer
- **Bundle Size**: Additional overhead for small apps
- **Flexibility**: More opinionated than Redux/Zustand

## 🧪 Testing Strategy

### Testing Tools
```javascript
// package.json dependencies
{
  "@testing-library/react": "^16.3.0",
  "@testing-library/jest-dom": "^6.8.0", 
  "@testing-library/user-event": "^13.5.0"
}
```

### Component Testing Example
```javascript
import { render, screen } from '@testing-library/react';
import { RelayEnvironmentProvider } from 'react-relay';
import { createMockEnvironment } from 'relay-test-utils';
import TodoList from './TodoList';

test('renders todo list', () => {
  const environment = createMockEnvironment();
  
  render(
    <RelayEnvironmentProvider environment={environment}>
      <TodoList />
    </RelayEnvironmentProvider>
  );
  
  expect(screen.getByText('Todo List')).toBeInTheDocument();
});
```

## 🔒 Security Considerations

### Environment Variables
- **API URLs**: Environment-specific configuration
- **No Secrets**: No API keys or secrets in frontend code
- **Build-time Injection**: Environment variables baked into build

### CORS & Network Security
- **Same Origin**: API calls from same domain in production
- **CORS Headers**: Backend properly configured for frontend domain
- **Content Security Policy**: Nginx can add CSP headers

### Input Validation
- **Client-side Validation**: Form validation before submission
- **Server-side Validation**: Backend validates all inputs
- **GraphQL Types**: Strong typing prevents injection

## 📊 Performance Optimization

### Bundle Analysis
```bash
# Analyze bundle size
npm run build
npx serve -s build  # Serve production build locally
```

### Optimization Strategies
- **Code Splitting**: React lazy loading for routes
- **Tree Shaking**: Webpack removes unused code
- **Minification**: Production builds are minified
- **Compression**: Nginx gzip compression
- **Caching**: Browser caching for static assets

### Relay Performance Features
- **Query Deduplication**: Automatic duplicate query removal
- **Prefetching**: Relay can prefetch data for better UX
- **Subscription Cleanup**: Automatic cleanup prevents memory leaks
- **Optimistic Updates**: Immediate UI feedback

## 🚦 Monitoring & Debugging

### Development Tools
```javascript
// Relay DevTools integration
if (process.env.NODE_ENV === 'development') {
  // Enable Relay DevTools
  window.__RELAY_DEVTOOLS_HOOK__ = true;
}
```

### Error Boundaries
```javascript
class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.error('Error caught by boundary:', error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      return <h1>Something went wrong.</h1>;
    }
    return this.props.children;
  }
}
```

## 🔄 Deployment Pipeline

### Docker Build Process
1. **Install Dependencies**: `npm ci` for clean install
2. **Build Application**: `npm run build` creates optimized bundle
3. **Static Files**: Copy build output to Nginx
4. **Configuration**: Apply environment-specific config
5. **Health Checks**: Container health monitoring

### Environment Promotion
```bash
# Development
docker build -t todos-app:dev .

# Production
docker build -t todos-app:prod --build-arg ENV=production .
```

---

**Integration:** This frontend seamlessly integrates with the [Backend API](../Todos.Solution/README.md) through GraphQL, providing a complete full-stack solution.
