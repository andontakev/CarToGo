# 🚗 CarToGo – Rent-a-Car Web Application

## 📌 Overview

**CarToGo** is a web-based application for managing a car rental service.
It allows users to browse available vehicles and make reservations, while administrators manage cars, users, and bookings.

---

## 🎯 Project Purpose

The goal of this project is to create a complete **three-layered web application** that simulates a real-world rent-a-car system with authentication, role-based access, and reservation management.

---

## 🏗️ Architecture

The application follows a **three-tier architecture**:

* **Data Layer** – Handles database access using Entity Framework
* **Service Layer** – Contains business logic and validations
* **Presentation Layer** – Web interface for users and administrators

---

## ⚙️ Technologies Used

* **C#**
* **ASP.NET / ASP.NET Core**
* **Entity Framework**
* **SQL Database**
* **Git & GitHub**

---

## 👤 User Roles

### 🔹 User

* Register and log in
* Browse available cars
* Create reservation requests

### 🔹 Admin

* Manage users (CRUD)
* Manage cars (CRUD)
* View and approve reservations

---

## 🚘 Car Management

Each car contains:

* Brand
* Model
* Year
* Number of seats
* Description 
* Price per day

---

## 📅 Reservation System

Users can:

* Select a rental period
* View available cars for selected dates
* Create reservation requests

Admins can:

* View all reservations
* Approve or manage them

---

## 🧠 Key Features

* Authentication and authorization
* Role-based access control
* Full CRUD operations
* Reservation system with date validation
* Prevention of double bookings

---

## ⚠️ Validations

* Unique username, email, and personal ID (EGN)
* Valid email format
* Car cannot be reserved if already booked for the selected period

---

## ▶️ How to Run the Project

1. Clone the repository:

```bash
git clone https://github.com/andontakev/CarToGo.git
```

2. Open the project in **Visual Studio**

3. Configure the database connection string

4. Run migrations (if needed)

5. Start the application

---

## 📸 Demo

The application is demonstrated live, showcasing:

* User interface
* Reservation process
* Admin functionalities

---

## 📚 Future Improvements

* Online payments
* Car rating system
* Mobile-friendly design
* Notifications system

---

## 👥 Team

* Preslava Dobreva
* Andon Takev

---

## 📌 Notes

This project was developed as part of a course assignment and follows best practices for C# and web application development.
