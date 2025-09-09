# Contributing to Todos Full-Stack Application

Thank you for your interest in contributing! This document provides guidelines and information for contributors.

## 🚀 Quick Start for Contributors

### Prerequisites
- Docker and Docker Compose
- Node.js 18+ (for frontend development)
- .NET 9 SDK (for backend development)
- Git

### Development Setup
```bash
# Fork and clone the repository
git clone https://github.com/yourusername/todos-fullstack-app.git
cd todos-fullstack-app

# Run with Docker (easiest way)
cd Todos.Solution
docker compose up -d --build

# Or run locally for development
# Backend
cd Todos.Solution
dotnet run --project Todos.Api/src

# Frontend  
cd todos.app
npm install
npm start
```

## 🎯 How to Contribute

### Types of Contributions Welcome
- 🐛 Bug reports and fixes
- ✨ New features
- 📚 Documentation improvements
- 🎨 UI/UX enhancements
- ⚡ Performance optimizations
- 🧪 Test coverage improvements

### Contribution Process
1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Make** your changes
4. **Test** your changes thoroughly
5. **Commit** your changes (`git commit -m 'Add amazing feature'`)
6. **Push** to the branch (`git push origin feature/amazing-feature`)
7. **Open** a Pull Request

## 🏗️ Development Guidelines

### Backend (.NET)
- Follow Clean Architecture principles
- Write unit tests for business logic
- Use async/await patterns
- Follow C# naming conventions
- Update GraphQL schema if needed

### Frontend (React)
- Use functional components with hooks
- Follow React best practices
- Write Relay GraphQL queries properly
- Maintain responsive design
- Update tests for new components

### Database
- Create migrations for schema changes
- Ensure backward compatibility
- Test with both SQLite and PostgreSQL

### Docker
- Test containers locally before PR
- Optimize Dockerfile if making changes
- Update docker-compose.yml if needed

## 🧪 Testing

### Running Tests
```bash
# Backend tests
cd Todos.Solution
dotnet test

# Frontend tests  
cd todos.app
npm test

# Integration tests with Docker
docker compose up -d --build
# Run your integration tests
```

### Test Requirements
- All new features must include tests
- Bug fixes should include regression tests
- Maintain or improve code coverage

## 📝 Pull Request Guidelines

### PR Title Format
- Use conventional commits format
- Examples:
  - `feat: add user authentication`
  - `fix: resolve GraphQL query timeout`
  - `docs: update API documentation`
  - `refactor: improve error handling`

### PR Description Template
```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Documentation update
- [ ] Performance improvement
- [ ] Other (please describe)

## Testing
- [ ] Unit tests pass
- [ ] Integration tests pass
- [ ] Manual testing completed
- [ ] Docker containers build and run

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review completed
- [ ] Documentation updated
- [ ] No breaking changes (or clearly documented)
```

## 🎨 Code Style Guidelines

### Backend (C#)
```csharp
// Use PascalCase for public members
public class TodoService : ITodoService
{
    // Use camelCase for private fields with underscore
    private readonly ITodoRepository _repository;
    
    // Use async/await for I/O operations
    public async Task<List<TodoModel>> GetTodosAsync()
    {
        return await _repository.GetAllAsync();
    }
}
```

### Frontend (JavaScript/React)
```javascript
// Use camelCase for variables and functions
const todoItems = [];

// Use PascalCase for components
const TodoList = ({ todos }) => {
  // Use descriptive names
  const handleTodoComplete = (id) => {
    // Implementation
  };
  
  return (
    <div className="todo-list">
      {/* Component content */}
    </div>
  );
};
```

## 🐛 Bug Reports

### Bug Report Template
```markdown
**Describe the bug**
A clear description of the bug

**To Reproduce**
Steps to reproduce:
1. Go to '...'
2. Click on '...'
3. See error

**Expected behavior**
What should happen

**Screenshots**
If applicable

**Environment:**
- OS: [e.g. Windows 10]
- Browser: [e.g. Chrome 91]
- Docker version: [e.g. 20.10.7]
```

## 💡 Feature Requests

### Feature Request Template
```markdown
**Is your feature request related to a problem?**
Description of the problem

**Describe the solution you'd like**
Clear description of desired feature

**Additional context**
Any other context, mockups, or examples
```

## 🏆 Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes for their contributions
- GitHub contributors page

## 📞 Getting Help

- 💬 **Discussions**: Use GitHub Discussions for questions
- 🐛 **Issues**: Create GitHub Issues for bugs
- 📧 **Email**: anupdg@outlook.com for sensitive issues

## 📜 Code of Conduct

Please note that this project follows a standard Code of Conduct. By participating, you agree to uphold this code.

### Our Standards
- Be respectful and inclusive
- Focus on constructive feedback
- Accept responsibility for mistakes
- Show empathy towards community members

---

Thank you for contributing to make this project better! 🚀
