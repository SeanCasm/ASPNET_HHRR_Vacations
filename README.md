# ASPNET HHRR Vacations

## Web application
ASP.NET MVC web application for simple employee administration, using SQLServer Express and Entity Framework Core database first.

The application acts like a web portal for employees, where employees can request vacations and an administrator can approve or decline those requests. Adminsitrators also can **CREATE**, **READ**, **UPDATE** and **DELETE** employees data.

## User creator
Also a console application is included in the proyect just for create administrators or employees credentials.

Both applications **works** only in **localhost**.

## How to use
Firstable, you have to run the **databaseEnterpriseDB.sql** inside **Database** folder to create the **Enterprise** database required for this project.
- ASPNET_HHRR_Vacations
  - ...
  - ...
  - ...
  - Database
    - databaseEnterpriseDB.sql
   
    
>It's important to use Windows authentication and trust in server certificates

Then, you have to run the **schemaData.sql** inside the folder Database to replicate the database schema and data in your machine.
- ASPNET_HHRR_Vacations
  - ...
  - ...
  - ...
  - Database
    - schemaData.sql

>It's important to use Windows authentication and trust in server certificates

Then, you have to open the **ASPNET_HHRR_Vacations** solution and modify the **ConnectionStrings** Server name inside **appsettings.json**.
- ASPNET_HHRR_Vacations
  - ...
  - ...
  - ...
  - appsettings.json
    ```json
    "ConnectionStrings": {
      "EnterpriseDB": "Server=(REPLACE)\\SQLEXPRESS;Database=Enterprise;TrustServerCertificate=True;Integrated Security=true"
    }

Finally, run the web or console application with `dotnet run` inside **WebApplication** or **ConsoleApplication**.

Below are the administrator and employee credentials to test the application.

### Administrator
- Email: sebastian.cataldo@enterprise.com
- Password: asddsa123

### Employee
- Email: gustavo.rivera@enterprise.com
- Password: gustavo.rivera

### Note
Developed just for experience proof in ASP.NET environment.
