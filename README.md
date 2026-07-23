# 🤖 AI CV Analyzer

An AI-powered web application that analyzes resumes (CVs) and provides intelligent feedback to help candidates improve their chances of getting hired.

The platform allows users to upload their CV, extract important information, analyze skills and experience, and receive AI-generated recommendations based on job requirements.

---

## 🚀 Features

### 👤 User Features
- Upload CV files (PDF format)
- Extract CV information automatically
- Analyze skills, education, and experience
- Generate AI-powered CV feedback
- Identify strengths and improvement areas
- Receive recommendations for better job matching

### 🤖 AI Analysis
- Resume content understanding using AI APIs
- Skill extraction
- Experience evaluation
- CV quality assessment
- Personalized improvement suggestions

### 🔐 Security
- User authentication and authorization
- Secure password management using ASP.NET Core Identity
- Protected user data

### ⚙️ Administration
- Manage users
- Monitor CV analysis requests
- Manage system data

---

# 🏗️ Architecture

The project follows a clean and maintainable architecture based on ASP.NET Core principles.


AI CV Analyzer
```bash
├── Presentation Layer
│ └── ASP.NET Core Razor Pages / MVC
│
├── Business Logic Layer
│ └── Services and AI processing
│
├── Data Access Layer
│ └── Entity Framework Core
```

---

# 🛠️ Tech Stack

## Backend
- C#
- ASP.NET Core 8
- Entity Framework Core
- ASP.NET Core Identity

## Database
- PostgreSQL

## Frontend
- Razor Pages
- HTML
- CSS
- Bootstrap
- JavaScript

## AI Integration
- AI APIs for CV analysis and recommendations

## DevOps
- Docker
- Docker Compose

---

# 🗄️ Database

The application uses PostgreSQL with Entity Framework Core Code First approach.

Main entities:

- User
- CV
- CV Analysis
- Skills
- Recommendations

Database features:

✅ Relationships using EF Core  
✅ Migrations  
✅ Data validation  
✅ Secure user management  

---

# 🐳  Docker

## Prerequisites

Make sure you have:

- Docker Desktop installed
- .NET 8 SDK installed (for development)

---

# 🧩 Local Development Setup


```bash
git clone https://github.com/yourusername/AI-CV-Analyzer.git

cd AI-CV-Analyzer



dotnet ef database update
dotnet run
```


---
# 👨‍💻 Author

Hasan Jakmara

Computer Science Graduate

GitHub:
https://github.com/Hasan123-cs

LinkedIn:
www.linkedin.com/in/hasan-jakmara-68b012374

