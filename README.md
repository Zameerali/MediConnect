# MediConnect - Healthcare Management System

MediConnect is a robust, role-based healthcare management platform developed using the **ASP.NET Core 8.0 MVC** framework. It is designed to bridge the gap between patients and healthcare providers by automating appointment scheduling, profile management, and clinical workflows.

---

## 📖 Table of Contents
- [Core Features](#-core-features)
- [System Architecture](#-system-architecture)
- [Tech Stack](#-tech-stack)
- [Database Design](#-database-design)
- [Getting Started](#-getting-started)
- [Technical Implementation Details](#-technical-implementation-details)
- [Future Roadmap](#-future-roadmap)
- [Contributing](#-contributing)

---

## 🚀 Core Features

### 👤 Patient Module
Designed for a seamless user experience, allowing patients to take control of their healthcare journey.
- **Dynamic Dashboard:** Personalized view of upcoming health appointments and clinical status.
- **Smart Appointment Booking:** 
  - Choice of specialists (Cardiology, Dentistry, Neurology).
  - Specific date and time-slot selection (Morning, Afternoon, Evening sessions).
  - Reason-based booking for better clinical preparation.
- **Full Appointment Lifecycle (CRUD):**
  - **Create:** Book new consultations.
  - **Read:** Comprehensive history of all bookings with status tracking.
  - **Update:** Reschedule appointments or modify reason for visit.
  - **Delete:** Cancel appointments with instant state updates.
- **Profile Management:** Maintain up-to-date personal information.

### 👨‍⚕️ Doctor Module
A dedicated interface for medical professionals to manage their clinical day.
- **Clinical Dashboard:** High-level overview of daily patient load.
- **Appointment Management:** Review patient reasons and manage clinical availability.
- **Schedule Configuration:** Visual view of the doctor's weekly schedule.
- **Professional Profile:** Manage credentials and specialty information.

### 🔒 Security & Identity
- **Integrated Identity System:** Built on ASP.NET Core Identity for secure sign-in, registration, and session management.
- **Role-Based Layouts:** Distinct UI experiences for Patients and Doctors using specialized Razor layouts (`_LayoutPatient.cshtml`, `_LayoutDoctor.cshtml`).

---

## 🏗 System Architecture

The application follows the **Repository Pattern** combined with **Dependency Injection** to ensure a decoupled and testable codebase.

### Key Components:
- **Controllers:** Handle user interactions and route them to the appropriate services.
- **Models & Interfaces:** Define the data structure and the contracts for data operations.
- **Repositories:** Abstract the data access logic from the business logic, allowing for easier switching of database providers.
- **Views:** Highly modular Razor views using Bootstrap 5 for responsive design.

---

## 🛠 Tech Stack

| Layer | Technology |
| :--- | :--- |
| **Backend Framework** | ASP.NET Core 8.0 (MVC) |
| **Language** | C# 12 |
| **Database** | MS SQL Server (LocalDB) |
| **ORM** | Entity Framework Core |
| **Security** | ASP.NET Core Identity |
| **Frontend** | Razor Pages, Bootstrap 5, jQuery |
| **Client-side Logic** | JavaScript (ES6+) |

---

## 🗄 Database Design

The system utilizes two primary context layers:
1. **ApplicationDbContext:** Manages Identity-related data (Users, Roles, Claims).
2. **DBContext:** Manages business-specific data (Appointments).

### Appointment Schema:
- `Id`: Primary Key (Identity)
- `DoctorName`: String (Specialist name)
- `AppointmentDate`: DateTime
- `TimeSlot`: String (Session timing)
- `Reason`: String (Patient-provided details)
- `Status`: String (Pending/Confirmed/Cancelled)

---

## ⚙️ Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- SQL Server Express LocalDB

### Installation

1. **Clone & Restore:**
   ```bash
   git clone https://github.com/yourusername/mediconnect.git
   cd MediConnect
   dotnet restore
   ```

2. **Configure Database:**
   Update the connection string in `appsettings.json`.
   > **Note:** The `AppointmentRepository` currently uses a hardcoded LocalDB connection string in `DBContext.cs`. Ensure your LocalDB instance is running.

3. **Initialize Database:**
   ```bash
   dotnet ef database update
   ```

4. **Launch:**
   ```bash
   dotnet run
   ```

---

## 🛠 Technical Implementation Details

### Dependency Injection
The application leverages the built-in DI container to manage service lifetimes:
```csharp
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
```

### Data Access Implementation
The `AppointmentRepository` uses Entity Framework Core for robust data handling, supporting LINQ for efficient querying and state management.

### Validation Logic
- **Server-side:** Controller-level checks for null inputs and invalid date ranges.
- **Client-side:** HTML5 validation and Bootstrap-styled feedback for required fields.

---

## 📅 Future Roadmap
- [ ] **Unified DB Context:** Merge Identity and Business contexts for transactional consistency.
- [ ] **Real-time Notifications:** SignalR integration for appointment reminders.
- [ ] **Role-Based Authorization:** Implement `[Authorize(Roles = "Doctor")]` attributes.
- [ ] **File Uploads:** Support for patient medical records and prescriptions.
- [ ] **API Layer:** Expose RESTful endpoints for potential mobile application integration.

---

## 🤝 Contributing
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 🛡 License
Distributed under the MIT License. See `LICENSE` for more information.

---
*Created by the MediConnect Team - 2025*
