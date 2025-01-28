# IdentityService
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Team-One-Pos-Tech_SnackHub.ClientService&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Team-One-Pos-Tech_SnackHub.ClientService)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Team-One-Pos-Tech_SnackHub.ClientService&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Team-One-Pos-Tech_SnackHub.ClientService)

## Overview
IdentityService is a .NET service designed to manage client for a Snack Hub restaurant and Framup. This project includes an API, database context, and behavior tests to ensure the functionality of the system.

More details about SnackHub Project can be found [here](https://github.com/Team-One-Pos-Tech/SnackHub/wiki)

### The main gateway for the SnackHub project can be found [here](github.com/Team-One-Pos-Tech/SnackHub.ApiGateway)

## Getting Started

### Prerequisites

- .NET 8.0
- PostgreSQL
- Docker

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/Team-One-Pos-Tech/IdentityService.git
    cd IdentityService
    ```

2. Install dependencies:
    ```sh
    dotnet restore
    ```

### Running the Application

To run the application locally, use the following command:
```sh
cd /src/IdentityService.Api
dotnet run
```

### Running Tests

To run the tests, use the following command:
Ensure you are running Docker locally.
```sh
dotnet test
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.

