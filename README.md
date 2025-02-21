# BankApp

## 📌 Project Overview

BankApp is a simple banking system that provides basic functionalities such as creating accounts, managing balances, and processing transactions. The backend is built using **.NET 7** with **Entity Framework Core** and a **SQL Server** database. The system utilizes **stored procedures** for efficient database operations.

## 🏗️ Tech Stack

- **Backend:** .NET 7 (ASP.NET Web API)
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT (if implemented)

## 🔹 Features

- Create new bank accounts
- Update account details
- Retrieve bank and account details
- Perform deposits, withdrawals, and transfers
- Secure and efficient database operations using stored procedures

## ⚙️ Setup and Installation

### 1️⃣ Clone the Repository

```sh
git clone https://github.com/yourusername/BankApp.git
cd BankApp
```

### 2️⃣ Configure Database Connection

Modify the `appsettings.json` file with your SQL Server connection string:

```json
"ConnectionStrings": {
    "BankDB": "Server=your_server;Database=BankAppDB;User Id=your_user;Password=your_password;"
}
```

### 3️⃣ Run Database Migrations (If using EF Core)

```sh
dotnet ef database update
```

### 4️⃣ Run the Application

```sh
dotnet run
```

The API will be available at `http://localhost:5000` (or another port if configured).

## 📂 Database Schema

The database contains two main tables:

### 🏦 **BankDetails**

| Column   | Type         | Constraints     |
| -------- | ------------ | --------------- |
| BankID   | INT          | PRIMARY KEY     |
| BankName | VARCHAR(255) | NOT NULL        |
| ISFCCode | VARCHAR(50)  | UNIQUE NOT NULL |
| Branch   | VARCHAR(255) | NOT NULL        |
| City     | VARCHAR(100) | NOT NULL        |

### 👤 **AccountHolderDetails**

| Column            | Type          | Constraints                                   |
| ----------------- | ------------- | --------------------------------------------- |
| AccountID         | INT           | PRIMARY KEY, IDENTITY(1,1)                    |
| AccountHolderName | VARCHAR(255)  | NOT NULL                                      |
| Gender            | VARCHAR(10)   | CHECK (Gender IN ('Male', 'Female', 'Other')) |
| AccountNumber     | VARCHAR(50)   | UNIQUE, NOT NULL                              |
| BankID            | INT           | FOREIGN KEY REFERENCES BankDetails(BankID)    |
| Balance           | DECIMAL(18,2) | DEFAULT 0.00                                  |
| PANCard           | VARCHAR(10)   | UNIQUE, NOT NULL                              |
| AadharCard        | VARCHAR(12)   | UNIQUE, NOT NULL                              |
| PhoneNumber       | VARCHAR(20)   | UNIQUE, NOT NULL                              |
| Email             | VARCHAR(255)  | UNIQUE, NOT NULL                              |
| CreatedDate       | DATETIME      | DEFAULT GETDATE()                             |

## 🔧 Stored Procedures

The project uses stored procedures for database operations:

- `GetAllBankDetails` – Fetches all bank details.
- `GetAccountByID` – Retrieves an account by ID.
- `UpdateAccountDetails` – Updates account holder details.
- `DepositAmount` – Increases account balance.
- `WithdrawAmount` – Decreases account balance.
- `TransferAmount` – Transfers funds between two accounts.

## 📌 API Endpoints

### 1️⃣ **Bank API**

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| GET    | `/api/bank/getAll` | Get all banks     |
| POST   | `/api/bank/create` | Create a new bank |

### 2️⃣ **Account API**

| Method | Endpoint                | Description               |
| ------ | ----------------------- | ------------------------- |
| GET    | `/api/account/get/{id}` | Get account details by ID |
| POST   | `/api/account/create`   | Create a new account      |
| PUT    | `/api/account/update`   | Update account details    |
| PUT    | `/api/account/deposit`  | Deposit amount            |
| PUT    | `/api/account/withdraw` | Withdraw amount           |
| POST   | `/api/account/transfer` | Transfer funds            |

## 🚀 Future Enhancements

- Implement authentication (JWT-based login and user roles)
- Add logging and exception handling
- Improve API security with rate limiting
- Introduce a frontend (React/Angular) for UI

## 👨‍💻 Author

Developed by Gautam Jain

📧 Contact: gautamjain572@gmail.com

