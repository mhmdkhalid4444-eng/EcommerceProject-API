# Ecommerce API

This is an E-Commerce Web API built using ASP.NET Core following Clean Architecture principles. The project provides the basic features required for an online shopping system including authentication, product management, shopping basket, orders, and payment integration.

## Technologies

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Redis
- Stripe
- JWT Authentication
- AutoMapper
- Swagger

## Features

- User Registration and Login
- JWT Authentication
- Product, Brand and Type Management
- Shopping Basket using Redis
- Order Management
- Payment Integration with Stripe
- Delivery Methods
- Swagger API Documentation

## Project Structure

- Ecommerce.API
- Ecommerce.Application
- Ecommerce.Domain
- Ecommerce.Infrastructure

## How to Run

1. Clone the repository.
2. Restore the packages.
3. Update the connection strings in `appsettings.json`.
4. Add your Stripe Secret Key.
5. Run the database migrations.
6. Start the project.

## API Endpoints

The project includes endpoints for:

- Authentication
- Products
- Basket
- Orders
- Payments
- Delivery Methods

Swagger is available after running the project.

## Notes

This project was developed for learning and practicing ASP.NET Core Web API concepts such as Clean Architecture, Repository Pattern, Unit of Work, Specification Pattern, JWT Authentication, Redis, and Stripe integration.
