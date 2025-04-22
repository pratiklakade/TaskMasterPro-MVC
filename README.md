# 📝 TaskMasterPro - Task Management App (ASP.NET Core MVC)

TaskMasterPro is a modern, user-friendly task management web application built using **ASP.NET Core MVC**.  
It allows users to manage their daily tasks efficiently with features like categorization, prioritization, due dates, and an intuitive interface.

---

## 🚀 Features

- 🔐 User Registration & Login (Authentication)
- 📌 Create, Edit, and Delete Tasks
- 🗂 Task Categories (Personal, Work, Shopping, etc.)
- 🚦 Priority Levels (Low, Medium, High)
- 📅 Due Date Selection
- ⏰ Reminder-Friendly Layout
- 🎨 Clean and Polished UI with Bootstrap
- 📊 Enum Dropdowns for Priority & Category
- 🔍 Task Detail View with Created Date
- ✅ Client-side & Server-side Validation
- 🛡️ Anti-forgery Tokens for Secure Forms

---

## 🧰 Tech Stack

- **.NET 8** (ASP.NET Core MVC)
- **Entity Framework Core**
- **SQL Server**
- **Razor Views**
- **Bootstrap 5**
- **C#**
- **Visual Studio 2022**

---

## 🖥️ Screenshots

📌 _(Add screenshots here after UI completion)_  
For example:
- Dashboard View  
- Create/Edit Task Form  
- Task Detail Page  
- Responsive Design on Mobile

---

## 📁 Folder Structure (MVC)
TaskMasterPro/ │ ├── Controllers/ │ └── TaskController.cs │ ├── Models/ │ ├── TaskModel.cs │ 
└── Enum/ │ ├── TaskCategory.cs │ └── PriorityLevel.cs │ 
├── Views/ │ ├── Task/ │ ├── Index.cshtml │ ├── Create.cshtml │ ├── Edit.cshtml │ ├── Details.cshtml │ └── Delete.cshtml │
├── Data/ │ └── ApplicationDbContext.cs │ └── wwwroot/ └── css, js, images

## ⚙️ Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/TaskMasterPro-MVC.git
cd TaskMasterPro-MVC

Run Migrations (if using EF Core)
Update-Database

✍️ Author
Pratik Lakade L.
.NET Developer | WPF | MVC | ASP.NET Core
LinkedIn (https://linkedin.com/in/pratik-lakade-9aaab6136)

💡 Future Enhancements
🔔 Email/SMS Reminders

📈 Dashboard with Task Stats

👥 Role-Based Authorization (Admin, Manager, User)

☁️ Azure Deployment

🤖 AI Suggestions (Smart Task Recommendations)

📱 PWA/Mobile version

