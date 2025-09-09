# Security Policy

## Supported Versions

We release patches for security vulnerabilities in the following versions:

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Reporting a Vulnerability

We take the security of our software seriously. If you believe you have found a security vulnerability, please report it to us as described below.

### How to Report

**Please do NOT report security vulnerabilities through public GitHub issues.**

Instead, please send an email to [anupdg@outlook.com] with the following information:

- Type of issue (e.g. buffer overflow, SQL injection, cross-site scripting, etc.)
- Full paths of source file(s) related to the manifestation of the issue
- The location of the affected source code (tag/branch/commit or direct URL)
- Any special configuration required to reproduce the issue
- Step-by-step instructions to reproduce the issue
- Proof-of-concept or exploit code (if possible)
- Impact of the issue, including how an attacker might exploit the issue

### Response Timeline

- We will acknowledge your email within 48 hours
- We will provide a detailed response within 7 days indicating next steps
- We will keep you informed of the progress towards a fix
- We may ask for additional information or guidance

### Security Best Practices for Users

When deploying this application:

1. **Environment Variables**: Never commit secrets or API keys
2. **Database**: Use strong database passwords in production
3. **HTTPS**: Always use HTTPS in production environments
4. **Updates**: Keep Docker images and dependencies updated
5. **Network**: Restrict network access to necessary ports only
6. **Backups**: Regular security-conscious backup procedures

### Known Security Considerations

This application includes the following security measures:

- **CORS Configuration**: Properly configured for production
- **Input Validation**: GraphQL schema validation
- **SQL Injection Protection**: Entity Framework Core parameterized queries
- **Container Security**: Non-root user execution in containers

### Vulnerability Disclosure Timeline

- Day 0: Security report received
- Day 1-7: Initial assessment and reproduction
- Day 8-30: Develop and test fix
- Day 31: Public disclosure (if appropriate)

We appreciate your efforts to responsibly disclose your findings.
