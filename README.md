# ObsidianInk

ObsidianInk is a digital bookstore and personal library built with ASP.NET Core, C#, Entity Framework Core, SQLite, and Razor Pages.

The project lets users browse a book catalog, sign in, purchase books, view their personal library, and inspect book details. It was built as a portfolio project to practice backend architecture, relational data modeling, authentication flow, DTOs, validation, and API-to-frontend communication.

## Main Features

- Book catalog with covers, authors, genres, prices, and details
- Personal library based on purchased books
- User registration and login
- Cookie-based authentication in the Razor Pages frontend
- REST API with Swagger/OpenAPI
- SQLite database for local execution
- Seeded local data for books, authors, genres, users, orders, and reviews
- Many-to-many relationships:
  - Books and Authors
  - Books and Genres
- Razor Pages frontend consuming the API with `HttpClient`

## Project Structure

```text
ObsidianInk/
├── Domain/             # Core entities: Book, Author, Genre, User, Order, Review
├── Infrastructure/     # EF Core DbContext and database seed data
├── ObsidianInk/        # ASP.NET Core Web API
└── Web/                # Razor Pages frontend
```

## Architecture Overview

The solution is separated into four main projects:

- `Domain`: contains the business entities used by the application.
- `Infrastructure`: contains `ObsidianInkContext`, the EF Core database context, and `DbInitializer` for local seeded data.
- `ObsidianInk`: exposes the REST API controllers for books, users, orders, authors, genres, and reviews.
- `Web`: contains the Razor Pages frontend. It does not connect directly to the database; it communicates with the API using `HttpClient`.

The request flow is:

```text
Browser
  -> Razor Page
  -> HttpClient
  -> API Controller
  -> ObsidianInkContext
  -> SQLite database
```

## Tech Stack

- C#
- ASP.NET Core
- Razor Pages
- Entity Framework Core
- SQLite
- Swagger/OpenAPI
- Bootstrap
- Cookie Authentication

## Main Domain Entities

- `Book`: represents a digital book with title, description, price, cover, and file URL.
- `Author`: represents a book author.
- `Genre`: represents a book category.
- `BookAuthor`: join entity for the many-to-many relationship between books and authors.
- `BookGenre`: join entity for the many-to-many relationship between books and genres.
- `User`: represents an application user.
- `Order`: represents a purchased book.
- `Review`: represents a book rating and comment.

## Database

The application uses Entity Framework Core with SQLite for local execution. The API registers `ObsidianInkContext` in `Program.cs` and uses the connection string from `appsettings.json`.

```json
"ConnectionStrings": {
  "ObsidianInkContext": "Data Source=obsidianink-demo.db"
}
```

`DbInitializer` creates local starter data when the database is empty. This includes authors, genres, books, users, orders, and reviews.

## Authentication Flow

The login flow is split between the API and the Web project:

1. The user submits email and password from the Razor Pages login form.
2. The Web project sends the credentials to `api/User/login`.
3. The API validates the credentials and returns an `AuthResponseDto`.
4. The Web project creates an authentication cookie with claims such as name, email, role, and user id.
5. Pages such as `Library` use `[Authorize]` and the `UserId` claim to load the authenticated user's purchased books.

The current authentication flow is intentionally lightweight for a portfolio project. A production system should use password hashing and a mature identity solution such as ASP.NET Core Identity.

## Important Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Book/all` | Returns all books |
| GET | `/api/Book/{id}` | Returns one book by id |
| POST | `/api/Book` | Creates a book |
| GET | `/api/Author` | Returns authors |
| GET | `/api/Genre` | Returns genres |
| POST | `/api/User/register` | Registers a user |
| POST | `/api/User/login` | Authenticates a user |
| POST | `/api/Order` | Creates an order |
| GET | `/api/Order/user/{userId}` | Returns orders for a user |
| GET | `/api/Review/book/{bookId}` | Returns reviews for a book |

## Demo Accounts

```text
User:
Email: demo@obsidianink.com
Password: Demo123!

Admin seed user:
Email: admin@obsidianink.com
Password: Admin123!
```

## How to Run

Clone the repository:

```bash
git clone https://github.com/CraftByteZ/ObsidianInk.git
cd ObsidianInk
```

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the API:

```bash
dotnet run --project ObsidianInk --launch-profile https
```

Open Swagger:

```text
https://localhost:7144/swagger
```

Run the Web frontend in another terminal:

```bash
dotnet run --project Web --launch-profile https
```

Open the Web app:

```text
https://localhost:7139
```

## What This Project Demonstrates

- Building an ASP.NET Core Web API
- Creating a Razor Pages frontend
- Connecting a frontend to an API with `HttpClient`
- Using Entity Framework Core with a relational database
- Modeling one-to-many and many-to-many relationships
- Using DTOs to shape API responses
- Creating a basic authentication flow with cookies and claims
- Loading local seed data for repeatable testing
- Separating responsibilities across multiple projects

## Possible Improvements

- Add password hashing with `PasswordHasher<User>` or move to ASP.NET Core Identity
- Add backend validation to prevent duplicate purchases
- Add review rules, such as only allowing reviews for purchased books
- Add a remove-from-library flow by deleting or canceling an order
- Add search and genre filtering in the catalog
- Add migrations for PostgreSQL, MySQL, or SQL Server
- Add automated tests for controllers and business rules
- Add role-based admin pages for managing books
