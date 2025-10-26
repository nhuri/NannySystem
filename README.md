# Nanny System - Windows .NET Project

## Overview
Nanny System is a comprehensive management application built with C# and WPF. This project provides a functional system for managing nannies, families, and schedules. It was designed as a reusable, extendable template for future projects, demonstrating modern .NET development practices, object-oriented programming, and a multi-tier architecture.

## Features
- **User Management**: Register and manage nannies and families with role-based access.
- **CRUD Operations**: Full create, read, update, and delete functionality for all core entities.
- **Multi-Tier Architecture**: Separation of concerns across Presentation Layer (WPF UI), Business Logic Layer, and Data Access Layer.
- **Threading**: Implements concurrent operations for improved performance.
- **WPF User Interface**: Interactive and responsive GUI with controls, layouts, and data binding.
- **Data Handling**: XML and LINQ-to-XML integration for flexible data manipulation.
- **Design Patterns**: Factory and Singleton patterns are used where appropriate.
- **Exception Handling**: Robust handling of runtime errors across all layers.

## Technology Stack
- **Language**: C#
- **Framework**: .NET (Windows Desktop Application)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Data Storage**: XML files and in-memory data structures
- **Concurrency**: Threads
- **Patterns**: Factory, Singleton
- **Tools**: Visual Studio, .NET runtime

## Project Structure
- **Models**: Defines the core data entities.
  - `Nanny`: Stores details like Name, Age, Experience, Certifications.
  - `Family`: Contains family information and associated children.
  - `Child`: Represents child information such as Name, Age, and special needs.
  - Other models follow the same pattern for maintainable data handling.
- **Views**: WPF UI screens for interacting with the system.
- **ViewModels**: Implements logic for binding between UI and data models.
- **Controllers/Services**: Business logic for handling operations like adding, editing, or deleting entities.
- **Data Access**: Reads/writes to XML and manages in-memory collections.

## Usage
1. **Launch the application** via Visual Studio.
2. **Register or login** as an admin or user with appropriate permissions.
3. **Manage entities**:
   - Add/Edit/Delete nannies, families, or children.
   - Search and filter records.
4. **Data persistence**:
   - All data is stored in XML files for simplicity.
   - LINQ-to-XML is used to query and manipulate data efficiently.

## Example
```csharp
// Adding a new nanny
Nanny newNanny = new Nanny { Name = "Sara", Age = 30, Experience = 5 };
NannyService.AddNanny(newNanny);

// Retrieve all nannies
var nannies = NannyService.GetAllNannies();
Testing
Functionality was manually tested in the application.

Threading operations were verified for concurrency.

XML data read/write confirmed through sample test cases.

Extensibility
The project is designed to be reused as a template for similar management systems.

Can be extended to integrate databases (SQL Server), authentication modules, or additional features.

Follows a multi-tier approach to simplify future enhancements.

Conclusion
This project showcases practical C# programming, WPF UI design, and solid software architecture practices. It demonstrates the ability to design and implement a complete, functional application suitable for real-world scenarios.
