Employee Management System
Overview
The Employee Management System is a web application built using ASP.NET 8.0. It is designed to manage employee information efficiently, offering features such as role-based authorization, account lockout, password recovery, and open authentication. The application leverages Entity Framework for database operations and Identity Server for authentication and authorization.

Features
Role-Based Authorization: Manage access to different parts of the application based on user roles.
Claim-Based Authorization: Implement fine-grained access control using user claims.
Account Lockout: Protect user accounts from brute-force attacks by locking them after multiple failed login attempts.
Password Recovery: Allow users to recover their accounts by resetting their passwords.
Password Change: Enable users to change their passwords securely.
Open Authentication: Support for logging in using Facebook and Gmail accounts.
Technologies Used
ASP.NET 8.0: The primary framework for building the web application.
Entity Framework: Used for interacting with the SQL Server 2019 database.
SQL Server 2019: The database server for storing application data.
Identity Server: Used for managing authentication and authorization.
Getting Started
Prerequisites
.NET 8.0 SDK
SQL Server 2019
Visual Studio 2022
Installation
Clone the repository:

sh
Copy code
git clone https://github.com/Usman9108/EmployeeManagementSystem.git
cd employee-management-system
Configure the database:

Update the connection string in appsettings.json to point to your SQL Server instance:
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}
Apply migrations:

sh
Copy code
dotnet ef database update
Run the application:

sh
Copy code
dotnet run
Authentication Setup
Facebook Authentication:

Register your application on the Facebook Developer Portal.
Update the Facebook section in appsettings.json with your App ID and App Secret:
json
Copy code
"Authentication": {
  "Facebook": {
    "AppId": "your_facebook_app_id",
    "AppSecret": "your_facebook_app_secret"
  }
}
Gmail Authentication:

Register your application on the Google Developer Console.
Update the Google section in appsettings.json with your Client ID and Client Secret:
json
Copy code
"Authentication": {
  "Google": {
    "ClientId": "your_google_client_id",
    "ClientSecret": "your_google_client_secret"
  }
}
Usage
Access the application at http://localhost:5000.
Use the registration page to create a new user account.
Use the login page to authenticate using your email/password or via Facebook/Gmail.
Access role-specific features based on your assigned roles.
Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.
