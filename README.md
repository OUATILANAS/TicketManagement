# Web Application

## Overview

This is a web application built with a .NET 8 backend and an Angular frontend, utilizing SQL Server as the database.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET 8 SDK installed
- Node.js and npm installed
- SQL Server installed
- Angular CLI installed

## Installation

Follow these steps to set up the application:

### Backend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/OUATILANAS/TicketManagement
   
2. Navigate to the backend directory:
   
  cd TicketManagement/backend/api

3. Restore the dependencies:
   
  dotnet restore

4. Configure the database connection in appsettings.json:

  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
    }
  }

   SQL Server Connection String Template (WINDOWS):  
   "Data Source={PCNAME}\\SQLEXPRESS;Initial Catalog={DATABASENAME};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

   Azure Edge Connection String Template (MAC OS ARM):
   "Data Source=localhost;Initial Catalog=finshark;User Id=sa;Password=MyPass@word;Integrated Security=True;TrustServerCertificate=true;Trusted_Connection=false"

5. Run the migrations to set up the database:
   
   dotnet ef database update

6. Start the backend application:

   dotnet run

   The backend API will be available at http://localhost:5296 . 


### Frontend Setup

1. Navigate to the frontend directory:

   cd ../../frontend

   cd TicketManagementApp

3. Install the frontend dependencies:

   npm install

4. Start the frontend application:

   ng serve

5. Open your browser and navigate to http://localhost:4200 to view the application.

### Usage

Once both the backend and frontend are running, you can interact with the application through your web browser.

### API Documentation

API documentation is available using Swagger at:

http://localhost:5296/swagger

### Demo




https://github.com/user-attachments/assets/f31d0ba0-97b3-4265-8d11-a4130e8af974

