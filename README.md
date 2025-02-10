# HotelBookingApp# 🏨 Hotel Booking Application

A web application for hotel booking management with Angular frontend and C# backend.

![Hotel Booking App](image/logo.png) <!-- Add a screenshot of your application here -->

## ✨ Features

DONE

- 📅 View Booking History
- 👥 User registration and authentication
- 📝 Booking management
- 📱 Responsive user interface

TO-DO

- Implement the booking system in different bussiness logic way
- Add room update/create on the frontend
- Add booking status update on the frontend

## 🚀 Technologies Used

- **Backend Framework:** .NET
- **Programming Language:** C#
- **Database:** SSMS
- **ORM:** Entity Framework
- **Frontend:** Angular
- **Authentication:** JWT
- **Version Control:** Git

## 📋 Prerequisites

- Visual Studio 2019/2022
- Node.js (v16+)
- Angular CLI
- .NET SDK
- SQL Server
- Git

## 🛠️ Installation

1. Clone the repository

```bash
git clone https://github.com/GregKorf/HotelBookingApp.git
```

## Backend Setup

1. Navigate to the Backend file

```bash
cd Backend
```

2. Open appsettings.json & Configure connection string

3. Install Dependencies

```bash
dotnet restore
```

4. Database Migration

```bash
dotnet ef database update
```

5. Run Application

```bash
dotnet run
```

It should run on port: 5002

You can check the api doc at https://localhost:5002/swagger/index.html

## Frontend Setup

1. Open the Frontend file into VS Code

2. Type npm install at the terminal

```bash
    npm install
```

3. Run the Frontend

```bash
 ng serve
```

It should open at port: 4002

## Customer Page

Landing Page:
image/landing.png

Login Page:
image/login.png

Singup Page:
image/signup.png

Customer Booking history:
image/history.png

Admin Landing Page:
image/admin-landing.png

Admin Manage Bookings Page:
image/admin-manage-bookings.png

## License

[MIT](https://choosealicense.com/licenses/mit/)
