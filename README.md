# BookHub API

BookHub API is a RESTful web service designed to manage a book store's core operations, including book management, user authentication, and transaction processing. Utilizing .NET 6, Entity Framework Core for data access, and JWT for secure authentication, BookHub API offers a solid foundation for building robust book store applications.

## Features

- **Book Management**: Create, read, update, and delete (CRUD) operations for books.
- **User Authentication**: Secure user registration and login process using JWT tokens.
- **Transaction Processing**: Support for buying and selling books with stock management.
- **Role-Based Access Control**: Distinct access levels for admins and common users.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later / Visual Studio Code
- MySQL Server
- Docker (optional, for running MySQL in a container)

### Setup

1. **Clone the Repository**

    ```
    git clone <repository-url>
    ```

2. **Install Dependencies**

    Navigate to the project directory and restore the .NET dependencies:

    ```
    cd BookHubAPI
    dotnet restore
    ```

3. **Configure Database**

    Update `appsettings.json` or `appsettings.Development.json` with your MySQL connection string:

    ```json
    "ConnectionStrings": {
        "BookHubDatabase": "server=localhost;port=yourmysqlport;database=bookhub;user=myuser;password=mypassword"
    }
    ```

    Run MySQL in Docker (optional):

    ```
    docker run --name mysql -e MYSQL_ROOT_PASSWORD=mypassword -p 3306:3306 -d mysql:latest
    ```

4. **Apply Migrations**

    Use EF Core migrations to setup your database schema:

    Create migration in the BookHub.Infrastructure directory:
    
    ```
    dotnet ef migrations add Migration_Message --project BookHub.Infrastructure/BookHub.Infrastructure.csproj --startup-project BookHub.API/BookHub.API.csproj
    ```

    Applay migration:
    
    ```
    dotnet ef database update --project BookHub.Infrastructure/BookHub.Infrastructure.csproj --startup-project BookHub.API/BookHub.API.csproj
    ```

5. **Run the Application**

    ```
    dotnet run
    ```

    Access the Swagger UI at `http://localhost:<port>/swagger`.

### Project Structure

- **BookHub.API**: The entry point of the application hosting the web service.
- **BookHub.Core**: Contains the core business logic, entities, and interfaces.
- **BookHub.Infrastructure**: Manages data access using EF Core and repository patterns.
- **BookHub.Services**: Implements the application's business logic.
- **BookHub.Identity**: Handles authentication and authorization logic.
- **BookHub.Common**: Provides common utilities and helpers used across the application.

## Usage

Refer to the Swagger UI for detailed API documentation and endpoint testing. Here's an example of how to authenticate and access a secured endpoint:

1. **Login**: Use the `/api/users/login` endpoint to obtain a JWT token.

2. **Authorize**: Click the "Authorize" button in Swagger UI and enter the JWT token.

3. **Access Secured Endpoints**: With the token set, you can now access secured endpoints.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or create issues for bugs and feature requests.

## License

[MIT License](LICENSE)
