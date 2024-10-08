markdown
Book Library API

Overview
The Book Library API is a RESTful service built with ASP.NET Core that allows users to manage a book library. It supports basic CRUD operations for book management.

Technologies
- C#
- .NET 6
- Entity Framework Core (In-memory database)
- Swagger for API documentation
- xUnit for unit testing

 Getting Started

 Prerequisites
- [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- A code editor such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

 Installation

1. Clone the repository if uploaded on git or download zipfile
```bash
git clone https://github.com/PatriciaAI/BookLibrary.git
cd book-library-api
```

2. Restore NuGet packages
```bash
dotnet restore
```

3. Run the application
```bash
dotnet run
```

 API Endpoints
- `GET /api/books` - Retrieves all books
- `GET /api/books/{id}` - Retrieves a specific book by ID
- `POST /api/books` - Adds a new book
- `PUT /api/books/{id}` - Updates an existing book
- `DELETE /api/books/{id}` - Deletes a book

 API Documentation
Swagger UI is available at `https://localhost:5001/swagger` for interactive API documentation.

 Running Tests
To execute the unit tests:
```bash
dotnet test
```

 Conclusion
This Book Library API serves as a foundation for managing a book library and can be extended with additional features such as user authentication and book borrowing functionality.
