# BankApp

## 📌 Project Overview

BankApp is a simple banking system that provides basic functionalities such as creating accounts, managing balances, and processing transactions. The backend is built using **.NET 7** with **Entity Framework Core** and a **SQL Server** database. The system utilizes **stored procedures** for efficient database operations.

## 🏗️ Tech Stack

- **Backend:** .NET 7 (ASP.NET Web API)
- **Database:** SQL Server
- **ORM:** Entity Framework Core

## 🔹 Features

- Create new bank accounts
- Update account details
- Retrieve bank and account details
- Perform deposits, withdrawals
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

The API will be available at `http://localhost:7220`

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

The project uses the following stored procedures for database operations:

### **Bank Details**
- `GetAllBankDetails` – Fetches all bank details.
- `InsertBankDetails` – Inserts a new bank record.
- `UpdateBankDetails` – Updates an existing bank record.

### **Account Holder Details**
- `GetAllAccountHolders` – Retrieves all account holders.
- `InsertAccountHolderDetails` – Inserts a new account holder.
- `UpdateAccountHolderDetails` – Updates account holder details.

### **Transactions**
- `WithdrawAmount` – Processes a withdrawal request.
- `DepositAmount` – Processes a deposit request.

## 📌 API Endpoints

### 1️⃣ **Bank API**

| Method | Endpoint                                         | Description       |
| ------ | -------------------------------------------------| ----------------- |
| GET    | `/api/BankAppCotroller/bank/Get-Bank-details`    | Get all banks     |
| POST   | `/api/BankAppCotroller/bank/Add-Bank-details`    | Create a new bank |
| PUT    | `/api/BankAppCotroller/bank/Update-Bank-details` | Update bank info  |

### 2️⃣ **Account API**

| Method | Endpoint                                                      | Description               |
| ------ | ------------------------------------------------------------- | ------------------------- |
| GET    | `/api/BankAppCotroller/Account/Get-AccountHolders-details`    | Get all account details   |
| POST   | `/api/BankAppCotroller/Account/Add-AccountHolders-details`    | Create a new account      |
| PUT    | `/api/BankAppCotroller/Account/Update-AccountHolders-details` | Update account details    |
| POST   | `/api/BankAppCotroller/Account/Withdraw-Amount`               | Deposit amount            |
| POST   | `/api/BankAppCotroller/Account/Deposit-Amount`                | Withdraw amount           |

## 🚀 Future Enhancements

- Add Remaning Balance API
- Implement authentication (JWT-based login and user roles)
- Add logging and exception handling
- Improve API security with rate limiting
- Introduce a frontend (Angular) for UI

## 👨‍💻 Author

Developed by Gautam Jain

📧 Contact: gautamjain572@gmail.com

