# Hospital Management System


## 📌 Project Description

This project is a Hospital Management System, demonstrating basic CRUD (Create, Read, Update, Delete) operations with a focus on the modular monolith software architecture. It's built using .NET 8 and adheres to best practices such as Domain-Driven Design (DDD), Clean Architecture, and Vertical Slice Architecture, incorporating a range of modern development patterns and libraries.
This repository showcases how to structure a large application as a modular monolith, where different functionalities are grouped into independent modules that can be developed and deployed separately but reside within the same codebase.  This provides the benefits of modularity and maintainability while avoiding the complexity of a distributed microservices architecture (at least initially).

## 🏗️ Architecture

The architecture of the system follows a modular monolith approach, with emphasis on DDD, Clean Architecture, and Vertical Slice Architecture.

The key components are:

*   **Main API:** The entry point for the application. This is where the ASP.NET Core application is hosted and exposes the endpoints.
*   **Modules:** Each module represents a distinct feature or area of functionality within the hospital management system. Examples include:
    *   Department Module
    *   Doctors Module
    *   Patients Module
    *   Appointments Module
    *   Authentication Module
*   **Contracts:** Each module has its own `Contracts` project/folder that defines the interfaces, DTOs (Data Transfer Objects), and events used for communication between modules.  This promotes loose coupling.
*   **Shared Components:** This area contains code that is shared across multiple modules.
    *   `Shared Project`: Contains core shared types, extensions, and helpers.
    *   `Shared.Messaging`:  Contains common messaging interfaces and infrastructure (e.g., message contracts, RabbitMQ configuration). This uses MassTransit.
*   **Vertical Slice Architecture:**  Each feature is implemented as a "slice" through the application, containing all the necessary components (API endpoint, command handler, domain logic, data access) within its module.  This emphasizes feature cohesion.
*   **Clean Architecture:** The application is structured into layers (Presentation, Application, Domain, Infrastructure) with dependencies flowing inward, promoting testability and maintainability.

## 🚀 Technologies Used

*   **.NET 8:** The core framework for building the application.
*   **ASP.NET Core:**  Used for building the RESTful API.
*   **EF Core (Entity Framework Core):** Object-Relational Mapper (ORM) for database interactions.
*   **MediatR:**  A library for implementing the Mediator pattern, enabling in-process messaging and decoupling between components.
*   **Carter:** A lightweight framework that simplifies building API endpoints using a convention-based approach.
*   **Mapster:** A fast object-to-object mapper.
*   **MassTransit:** A free, open-source distributed application framework for .NET.  For implementing message-based communication between modules. This is especially useful if you later decide to break the monolith into microservices.
*   **Domain-Driven Design (DDD):** A software development approach that focuses on modeling the domain logic and business rules.
*   **Clean Architecture:**  A software architecture that separates the concerns of the application into distinct layers.
*   **Vertical Slice Architecture:**  Organizes code around features, rather than layers.

## 🔧 Setup & Installation

1.  **Clone the repository:**
    ```bash
    git clone [repository URL]
    cd HospitalManagementSystem
    ```


2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```

3.  **Configure the database:**
    *   Update the database connection string in the `appsettings.json` file of the `HospitalManagementSystem.MainAPI` project.
    *   Apply migrations:
        ```bash
        dotnet ef database update -p HospitalManagementSystem.MainAPI -s HospitalManagementSystem.MainAPI
        ```

4.  **Run the application:**
    ```bash
    dotnet run --project HospitalManagementSystem.MainAPI
    ```

5.  **Access the API:** Open your browser and navigate to `https://localhost:[port]` (replace `[port]` with the port number configured in your `appsettings.json`).

## 📌 Features

Each module is a self-contained unit with its own:

*   **Domain Model:** Entities, value objects, and domain services that represent the module's business logic.
*   **Application Logic:** Use cases, command handlers, and query handlers that orchestrate the domain logic.
*   **Infrastructure:** Data access, external service integrations, and other infrastructure concerns.
*   **Contracts:** Defines the public interface of the module, including DTOs and events.

Modules communicate with each other through well-defined contracts, typically using MediatR for in-process messaging or MassTransit for asynchronous message-based communication.
