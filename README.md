# PubsData CRUD Application

This project is a **CRUD web application** built using **ASP.NET Core MVC (C#)** with a SQL Server `pubs` sample database.  
It provides full functionality to **search, list, add, edit, and delete** records for Titles, Authors, Publishers, and Sales.

---

## Features
- Search & listing pages for:
  - **Titles**
  - **Authors**
  - **Publishers**
  - **Sales**
- Create, Read, Update, Delete operations for all entities
- Stored procedures used for all database interactions
- Clean UI with **Bootstrap, jQuery, and CSS**
- Structured in MVC pattern with separation of concerns

---

## Tech Stack
- **Backend:** ASP.NET Core MVC (C#)
- **Frontend:** Bootstrap, jQuery, CSS
- **Database:** SQL Server (`pubs` sample DB)
- **Data Access:** Stored Procedures

---

## Setup Instructions

### 1. Clone Repository
```bash
git clone https://github.com/02BHAVANI/PubsData-Crud.git
cd PubsData-Crud
```

### 2. Database Setup

Download the Pubs sample database.
Open SQL Server Management Studio (SSMS).
Run the instpubs.sql script to create the pubs database on your local SQL Server instance.

### 3. Configure Connection String

Open the file appsettings.json.
Update the connection string to point to your SQL Server instance:

```json { "ConnectionStrings": { "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=pubs;Trusted_Connection=True;MultipleActiveResultSets=true" } } ```

Replace YOUR_SERVER_NAME with your actual SQL Server instance name.

### 4. Restore Dependencies

Make sure you have the .NET SDK installed. Restore required packages:

```dotnet restore```

### 5. Run the Application
```dotnet run```
