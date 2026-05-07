# 💳 Digital Wallet API

A robust and secure backend API built with **ASP.NET Core** for managing digital wallets, facilitating secure peer-to-peer transfers, and tracking transaction history with precision.

## 🚀 Key Features

-   **Secure Authentication**: Implemented using **JWT (JSON Web Tokens)** with industry-standard security configurations.
-   **ACID Transactions**: Financial integrity ensured via **Database Transactions (Commit/Rollback)** to prevent data loss during transfers.
-   **Advanced Filtering & Pagination**: Optimized transaction history retrieval using `IQueryable` for dynamic filtering by date and type.
-   **Clean Architecture**: Feature-based folder structure for better maintainability and scalability.
-   **Myanmar Timezone Support**: Custom helper for localized time formatting (UTC to Myanmar Standard Time).

## 🛠️ Tech Stack

-   **Framework**: .NET 8/9 (ASP.NET Core Web API)
-   **Database**: SQL Server
-   **ORM**: Entity Framework Core (Code-First Approach)
-   **Security**: JWT Bearer Authentication, User Secrets for sensitive data management.
-   **Language**: C#

## ⚙️ Getting Started

### Prerequisites

-   [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 or later)
-   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation & Setup

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/DigitalWallet.API.git
    cd DigitalWallet.API
    ```

2.  **Configure User Secrets**:
    To keep your JWT Key secure during development, use the Secret Manager:
    ```bash
    dotnet user-secrets init
    dotnet user-secrets set "Jwt:Key" "YOUR_SUPER_SECRET_KEY_AT_LEAST_32_CHARS"
    dotnet user-secrets set "Jwt:Issuer" "DigitalWalletAPI"
    dotnet user-secrets set "Jwt:Audience" "DigitalWalletClient"
    ```

3.  **Database Migration**:
    Update your database using Entity Framework Core:
    ```bash
    dotnet ef database update
    ```

4.  **Run the application**:
    ```bash
    dotnet run
    ```

## 📖 API Documentation

### Authentication
-   `POST /api/auth/register` - Create a new user account and wallet.
-   `POST /api/auth/login` - Authenticate and receive a JWT Bearer Token.

### Transactions
-   `POST /api/transactions/transfer` - Securely send money to another user (Requires JWT).
-   `GET /api/transactions/history` - Get paginated transaction history with filters (Requires JWT).

#### Query Parameters for History:
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `PageNumber` | int | The page number to retrieve (default: 1) |
| `PageSize` | int | Number of items per page (default: 10, max: 100) |
| `FromDate` | string | Filter from this date (ISO format) |
| `ToDate` | string | Filter to this date (ISO format) |
| `Type` | Enum | Transaction type (Transfer, TopUp, etc.) |

## 🛡️ Security Implementation

This project follows professional security practices:
-   **Validation**: Uses `[FromQuery]`, `[FromBody]`, and `[FromRoute]` appropriately for data binding.
-   **Authorization**: All financial endpoints are protected with `[Authorize]` attributes.
-   **Error Handling**: Centralized result patterns to avoid exposing stack traces to the client.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
