Secure Clinic Management System
A robust, role-based clinic management system built with ASP.NET Core. This project focuses heavily on secure backend architecture, database integrity, and implementing custom authentication mechanisms to simulate enterprise-level applications.

🛡 Key Engineering Highlights
Instead of using default templates, this project was built from the ground up with a focus on security and data consistency:

Custom Authentication: Implemented a custom cookie-based authentication system using ASP.NET Core Claims Identity, avoiding standard out-of-the-box templates to demonstrate deep understanding of security flows.
Role-Based Access Control (RBAC): Enforced strict authorization policies across all controllers. Admins have full system access, while Doctors are strictly restricted to viewing and managing only their own appointments.
Data Integrity: Prevented critical business logic flaws (like double-booking an appointment) by implementing a unique composite index on (DoctorId, AppointmentDate) at the database level.
Database-First Migrations: Designed and managed a relational schema of 12 tables using Entity Framework Core Code-First approach, ensuring smooth version control for the database structure.
🛠 Tech Stack
Backend Framework: ASP.NET Core MVC
Language: C#
ORM: Entity Framework Core
Database: SQL Server / MySQL
Architecture: RESTful API principles, MVC Pattern, Repository Pattern (if used)
📊 System Architecture & Roles
The system processes requests through a strict middleware pipeline:HTTP Request -> Authentication Cookie Validation -> Claims Extraction -> Role-Based Authorization Check -> Controller Action -> Database Query

🚀 Getting Started
Prerequisites
Visual Studio 2022
SQL Server (LocalDB or Express)
Setup
Clone the repository:
git clone https://github.com/M0az2/clinic-management-system.git
Open ITIProject.sln in Visual Studio.
Update the connection string in appsettings.json to point to your local SQL Server instance.
Open the Package Manager Console and run:
bash

Update-Database
(This will apply the 12 migrations and seed the default Admin user).
Press F5 to run the application.
📂 Core Modules
Account Management: Secure login/logout flows with claims generation.
Doctor Management: CRUD operations protected by Admin policies.
Patient Scheduling: Appointment creation with database-level double-booking prevention.
