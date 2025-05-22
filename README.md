# TechFood

TechFood is a FIAP Post-Graduation project that aims to create a web application for a food delivery service. The project is developed using the dotnet core framework and utilizes the ASP.NET Core MVC architecture. The application is designed to be responsive and user-friendly, providing a seamless experience for both customers and restaurant owner. The project is built with a focus on clean architecture, DDD, and hexagonal architecture principles.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- AutoMapper
- SQL Server
- RadixUI
- TypeScript
- HTML/CSS

## Features

- Consumer self-ordering system
- Consumer registration and login
- Restaurant registration and login
- Menu browsing
- Order placement
- Payment integration (Mercado Pago)
- Order painel for consumers
- Admin panel for restaurant owners

### Project Structure

The project is organized into several key components, including:

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.

2. With docker installed in the project root run:

```bash
  docker-compose up -d
```

3. Container http addresses

- api swagger: http://localhost:5000/api/swagger/index.html
- app self-order: http://localhost:5000/self-order/
- app preparation-monitor: http://localhost:5000/preparation-monitor/
- app admin: http://localhost:5000/admin/
