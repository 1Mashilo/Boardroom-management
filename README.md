# Boardroom Management System

## Overview

The Boardroom Management System is an ASP.NET Core MVC application that streamlines the process of scheduling and managing boardroom reservations. Users can check boardroom availability, make reservations, and manage their bookings, while administrators have additional controls for managing boardroom details and viewing all reservations.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Completed Tasks](#completed-tasks)
- [Tasks To Be Completed](#tasks-to-be-completed)
- [Setup Instructions](#setup-instructions)
- [Contributing](#contributing)
- [License](#license)

## Features

- User registration and authentication using ASP.NET Identity and Microsoft Identity Client (MSAL)
- Role-based access control for regular users and admins
- CRUD functionality for boardroom and reservation management
- Email notifications for reservation confirmations and reminders
- Microsoft Graph API integration for scheduling boardroom reservations as calendar events
- Responsive front-end with Bootstrap for mobile compatibility

## Technologies Used

- **Frontend:** ASP.NET Core MVC, Bootstrap, JavaScript
- **Backend:** ASP.NET Core, Entity Framework Core, Microsoft Graph SDK
- **Database:** Microsoft SQL Server
- **Email Service:** PaperCut SMTP for development mail testing
- **Authentication:** Authentication and role-based authorization with ASP.NET Identity

## Completed Tasks

- Set up ASP.NET Core MVC project and configured database with Microsoft SQL Server
- Created models for Boardroom, Reservation, and User roles (Admin and Regular User)
- Implemented authentication and role-based authorization with ASP.NET Identity
- Integrated Microsoft Graph API for managing boardroom reservations as calendar events
- Developed CRUD operations for boardroom and reservation entities
- Configured email notifications for reservation confirmations using PaperCut SMTP
- Implemented core business logic for scheduling and preventing double-booking of boardrooms
- Built a responsive front-end with Javascript, CSS and Bootstrap 

## Tasks To Be Completed

- Finalize frontend components for:
  - Real-time availability checks
  - Calendar-based view for easy scheduling and management
- Enhance email notification logic for reminders and cancellation alerts
- Optional: Integrate FullCalendar for better visualization of boardroom availability and bookings

## Setup Instructions

To run this project locally, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/1Mashilo/Boardroom-management.git

##  ApplicationSettings.json
 ```bash
 {
   "ConnectionStrings": {
     "DefaultConnection": "Your SQL Server connection string"
   },

   "PaperCutSMTP": {
     "Host": "localhost",
     "Port": 25,
     "FromEmail": "no-reply@boardroom.com"
   }
 }
