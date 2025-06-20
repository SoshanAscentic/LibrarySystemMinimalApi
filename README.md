# Library Management System API

A comprehensive library management system built with **ASP.NET Core Minimal APIs** and **Entity Framework Core**. This system provides functionality for managing books, members, and borrowing operations in a modern, RESTful API architecture.

## 🚀 Features

### 📚 Book Management
- **CRUD Operations**: Add, view, update, and delete books
- **Search & Filter**: Find books by ID, category, author, or availability
- **Categories**: Fiction, History, and Children's books
- **Availability Tracking**: Real-time book availability status
- **Duplicate Prevention**: Unique constraint on title + publication year

### 👥 Member Management
- **Member Types**: Regular Members, Minor Staff, Management Staff
- **Role-Based Permissions**: Different capabilities based on member type
- **Authentication**: Login and signup functionality
- **Member Profiles**: Track borrowed books count and permissions

### 📖 Borrowing System
- **Borrow & Return**: Complete borrowing workflow
- **Borrowing Limits**: Maximum 5 books per member
- **Permission Checks**: Role-based borrowing permissions
- **Availability Validation**: Automatic availability management

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
LibrarySystemMinimalApi/
├── 📁 Api/                     # Endpoints & API configuration
├── 📁 Application/             # Business logic & services
├── 📁 Data/                    # Data access & repositories
└── 📁 Domain/                  # Entity models & business rules
```

### Technology Stack

- **Framework**: ASP.NET Core 8.0
- **API Style**: Minimal APIs
- **Database**: SQL Server with Entity Framework Core
- **ORM**: Entity Framework Core (Code First)
- **Mapping**: AutoMapper
- **Documentation**: Swagger/OpenAPI
- **Logging**: Built-in ASP.NET Core logging

## 🛠️ Setup Instructions

### Prerequisites

- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB, SQL Server Express, or full SQL Server)
- **Visual Studio 2022** or **VS Code** (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd LibrarySystemMinimalApi
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure database connection**
   
   Update `appsettings.json` with your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibrarySystemDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Create and seed the database**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - **Swagger UI**: `https://localhost:7xxx/` (opens automatically)
   - **API Base**: `https://localhost:7xxx/api`

## 📋 API Endpoints

### 🔐 Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/auth/signup` | Register a new member |
| `POST` | `/api/auth/login` | Authenticate a member |

### 📚 Books
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/books` | Get all books |
| `GET` | `/api/books/{bookId}` | Get book by ID |
| `POST` | `/api/books` | Add a new book |
| `DELETE` | `/api/books/{bookId}` | Remove a book |
| `GET` | `/api/books/available` | Get available books |
| `GET` | `/api/books/category/{category}` | Get books by category |
| `GET` | `/api/books/author/{author}` | Get books by author |
| `GET` | `/api/books/{bookId}/check-availability` | Check book availability |

### 👥 Members
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/members` | Get all members |
| `GET` | `/api/members/{memberId}` | Get member by ID |

### 📖 Borrowing
| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/borrowing/borrow` | Borrow a book |
| `POST` | `/api/borrowing/return` | Return a book |
| `GET` | `/api/borrowing/member/{memberId}` | Get member's borrowing status |

## 📝 Usage Examples

### Register a New Member
```bash
POST /api/auth/signup
Content-Type: application/json

{
  "name": "John Doe",
  "memberType": 0
}
```

**Member Types:**
- `0` = Regular Member (can borrow books)
- `1` = Minor Staff (can manage books, cannot borrow)
- `2` = Management Staff (can manage books and borrow)

### Add a New Book
```bash
POST /api/books
Content-Type: application/json

{
  "title": "The Hobbit",
  "author": "J.R.R. Tolkien",
  "publicationYear": 1937,
  "category": 0
}
```

**Categories:**
- `0` = Fiction
- `1` = History
- `2` = Child

### Borrow a Book
```bash
POST /api/borrowing/borrow
Content-Type: application/json

{
  "bookId": 1,
  "memberID": 1
}
```

### Search Books by Category
```bash
GET /api/books/category/Fiction
```

## 🗄️ Database Schema

### Books Table
| Column | Type | Description |
|--------|------|-------------|
| `BookId` | `int` | Primary key (auto-increment) |
| `Title` | `nvarchar(200)` | Book title |
| `Author` | `nvarchar(100)` | Author name |
| `PublicationYear` | `int` | Year of publication |
| `Category` | `int` | Book category (enum) |
| `IsAvailable` | `bit` | Availability status |

### Members Table
| Column | Type | Description |
|--------|------|-------------|
| `MemberID` | `int` | Primary key (auto-increment) |
| `Name` | `nvarchar(100)` | Member name |
| `BorrowedBooksCount` | `int` | Number of borrowed books |
| `MemberType` | `nvarchar(21)` | Discriminator for inheritance |

## 🔒 Business Rules

### Borrowing Rules
- **Maximum Books**: Members can borrow up to 5 books simultaneously
- **Permissions**: Only Regular Members and Management Staff can borrow books
- **Availability**: Books must be available to be borrowed
- **Return Validation**: Only borrowed books can be returned

### Member Permissions
| Member Type | Borrow Books | View Books | View Members | Manage Books |
|-------------|:------------:|:----------:|:------------:|:------------:|
| Regular Member | ✅ | ✅ | ✅ | ❌ |
| Minor Staff | ❌ | ✅ | ✅ | ✅ |
| Management Staff | ✅ | ✅ | ✅ | ✅ |

## 🛡️ Data Validation

### Book Validation
- **Title**: Required, 1-200 characters
- **Author**: Required, 1-100 characters
- **Publication Year**: 1450 to current year
- **Category**: Must be 0, 1, or 2
- **Uniqueness**: Title + Publication Year combination must be unique

### Member Validation
- **Name**: Required, 1-100 characters
- **Member Type**: Must be 0, 1, or 2

## 🌱 Seed Data

The application includes sample data for testing:

**Books:**
- The Great Gatsby (1925) - Fiction
- To Kill a Mockingbird (1960) - Fiction
- 1984 (1949) - Fiction
- A Brief History of Time (1988) - History
- The Very Hungry Caterpillar (1969) - Child

## 🚀 Development

### Adding New Features

1. **Domain Entities**: Add new entities in `Domain/Entities`
2. **Data Layer**: Create repositories in `Data/Repositories`
3. **Application Layer**: Implement services in `Application/Services`
4. **API Layer**: Add endpoints in `Api/Endpoints`

### Running Migrations
```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 📊 Health Check

The API includes health check endpoints:
- **API Info**: `GET /`
- **Health Status**: `GET /health`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For issues and questions:
1. Check the [Issues](../../issues) section
2. Review this README
3. Check the Swagger documentation at the root URL when running

## 🔗 Related Documentation

- [ASP.NET Core Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [Swagger/OpenAPI](https://swagger.io/)
