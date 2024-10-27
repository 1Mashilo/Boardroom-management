# Boardroom Management System

## Overview

The Boardroom Management System is an ASP.NET Core MVC application designed to facilitate the scheduling and management of boardroom reservations. Users can view available boardrooms, make reservations, and manage their bookings, while administrators can manage boardroom details and view all reservations.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Completed Tasks](#completed-tasks)
- [Tasks To Be Completed](#tasks-to-be-completed)
- [Setup Instructions](#setup-instructions)
- [Contributing](#contributing)
- [License](#license)

## Features

- User registration and authentication using ASP.NET Identity
- Role-based access control for regular users and admins
- CRUD functionality for boardroom and reservation management
- Email notifications for reservation confirmations and reminders
- Responsive front-end with Bootstrap

## Technologies Used

- **Frontend:** ASP.NET Core MVC, Bootstrap, JavaScript
- **Backend:** ASP.NET Core, Entity Framework Core
- **Database:** Microsoft SQL Server
- **Email Service:** PaperCut SMTP for mail authentication

## Completed Tasks

- Set up ASP.NET Core MVC project
- Configured database connection using Microsoft SQL Server
- Created models for Boardroom, Reservation, and User
- Assigned user roles (Admin and Regular User)
- Implemented mail authentication with PaperCut SMTP
- Scaffolded Boardroom and Reservation models
- Developed basic user interface for viewing and scheduling reservations

## Tasks To Be Completed

- Implement core business logic for scheduling and reservation management
- Develop the frontend components for:
  - Viewing available boardrooms
  - Scheduling reservations
  - Viewing and modifying existing reservations
- Integrate dynamic updates using JavaScript (e.g., calendar views, real-time availability checks)
- Implement scheduling logic to prevent double booking of boardrooms
- Add email notifications for upcoming reservations and cancellation confirmations
- Optional: Integrate a calendar UI (e.g., FullCalendar) for better visualization

## Setup Instructions

To run this project locally, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/1Mashilo/Boardroom-management.git
