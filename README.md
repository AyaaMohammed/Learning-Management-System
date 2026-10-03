# Mini LMS – Learning Management System

## 1. Overview

Mini LMS is a backend application built with ASP.NET Core Web API that provides a quiz management and assessment system.

The system supports two main roles:

* **Admin:** Manages questions, choices, quizzes, and monitors student performance.
* **Student:** Views available quizzes, answers questions, submits attempts, and views personal results.

The application focuses on maintainability, scalability, security, reliability, and separation of concerns.

## 2. Technologies Used

* C# / ASP.NET Core Web API
* .NET 8
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* xUnit
* Git

## 3. Architecture

The application follows Clean Architecture to separate business logic from infrastructure and presentation concerns.

### Project Structure

```text
MiniLMS/
│
├── API/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Filters/
│   ├── Extensions/
│   └── Program.cs
│
├── Application/
│   ├── Interfaces/
│   ├── DTOs/
│   ├── Features/
│   ├── Validators/
│   └── Common/
│       └── Results/
│
├── Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Exceptions/
│
├── Infrastructure/
│   ├── Persistence/
│   │   ├── Context/
│   │   ├── Configurations/
│   │   └── Migrations/
│   ├── Repositories/
│   └── UnitOfWork/
│
└── Tests/
    ├── UnitTests/
    └── IntegrationTests/
```

## 4. Authentication and Authorization

The application uses JWT Bearer Authentication.

* Users authenticate using their credentials.
* A JWT is generated after successful authentication.
* The token contains user identification, role, and tenant information.
* Role-based authorization protects application endpoints.
* Tenant identification is derived from authenticated user claims.

### Roles

| Role    | Permissions                                                                            |
| ------- | -------------------------------------------------------------------------------------- |
| Admin   | Manage questions, quizzes, and view performance statistics within their tenant.        |
| Student | View available quizzes, submit answers, and view personal results within their tenant. |

Students cannot access administrative endpoints or other students' results.

## 5. Technical Implementation

### 5.1 Correlation ID Middleware

A custom middleware generates or retrieves a Correlation ID for each incoming HTTP request.

**Responsibilities:**

* Track requests across the application.
* Connect related log entries.
* Simplify debugging and troubleshooting.
* Include the Correlation ID in response headers and logging scopes.

### 5.2 Global Exception Handler

A centralized exception handler manages unexpected application errors.

**Responsibilities:**

* Catch unhandled exceptions.
* Log exception details.
* Return consistent API error responses.
* Prevent exposing internal exception details to clients.

### 5.3 Validation Filter

A centralized validation filter handles invalid incoming requests before executing controller actions.

**Responsibilities:**

* Validate request models.
* Return consistent validation error responses.
* Prevent invalid data from reaching business logic.

### 5.4 Unit of Work Pattern

The Unit of Work pattern coordinates repository operations and database transactions.

**Responsibilities:**

* Coordinate multiple repository operations.
* Manage transaction boundaries.
* Commit changes atomically.
* Roll back changes when an operation fails.

This is particularly important when submitting quizzes and saving student answers.

### 5.5 Repository Pattern

The Repository pattern abstracts database access from business logic.

**Responsibilities:**

* Encapsulate data access operations.
* Separate queries from business rules.
* Improve maintainability and testability.

### 5.6 Result Pattern

The Result Pattern provides a consistent way to handle expected business failures without relying on exceptions for normal application flow.

A Result contains:

* Success or failure status.
* Returned data when successful.
* Error code and message when unsuccessful.

Unexpected exceptions are handled by the Global Exception Handler.

### 5.7 EF Core Migrations

Entity Framework Core Migrations manage database schema changes.

**Responsibilities:**

* Maintain database schema versions.
* Track changes to entities and relationships.
* Apply database updates consistently across environments.

### 5.8 Logging

Structured logging is used to monitor application behavior and troubleshoot errors.

Important information includes:

* Correlation ID.
* Request information.
* Operation details.
* Exception details.
* Relevant execution failures.

Sensitive information such as passwords and JWT tokens must not be logged.

### 5.9 Custom Exceptions

Custom exceptions represent specific application and business errors where exception-based handling is appropriate.

Examples:

* NotFoundException
* UnauthorizedException
* ForbiddenException
* ConflictException

These exceptions are translated into appropriate HTTP responses by the Global Exception Handler.

### 5.10 Multi-Tenancy

The application supports a multi-tenant architecture, allowing multiple organizations to use the same application while maintaining data isolation.

**Approach:**

* Each tenant has a unique TenantId.
* Users are associated with a specific tenant.
* Tenant ownership is maintained across relevant business entities.
* Tenant identification is derived from authenticated user claims.

**Data Isolation:**

* Admins can only manage questions and quizzes belonging to their tenant.
* Students can only access quizzes and attempts belonging to their tenant.
* Users cannot access another tenant's data.
* Tenant-aware queries and authorization checks enforce isolation.

**Why This Approach?**

A shared database with tenant-based data isolation reduces infrastructure complexity while allowing multiple organizations to use the same application.

It also provides a foundation for scaling the system as the number of tenants grows.

## 6. Functional Requirements

### Admin

| ID     | Feature              | Description                                           |
| ------ | -------------------- | ----------------------------------------------------- |
| ADM-01 | Create Questions     | Create questions with choices and one correct answer. |
| ADM-02 | Update Questions     | Edit questions and their choices.                     |
| ADM-03 | Remove Questions     | Remove questions from available learning content.     |
| ADM-04 | Student Performance  | View student scores and quiz results.                 |
| ADM-05 | Question Performance | View how students perform on individual questions.    |

### Student

| ID     | Feature           | Description                           |
| ------ | ----------------- | ------------------------------------- |
| STU-01 | Available Quizzes | View quizzes available to take.       |
| STU-02 | Answer Questions  | Select an answer for each question.   |
| STU-03 | Submit Quiz       | Submit answers for evaluation.        |
| STU-04 | View Results      | View personal scores and performance. |

### Authentication

| ID      | Feature        | Description                                                             |
| ------- | -------------- | ----------------------------------------------------------------------- |
| AUTH-01 | Login          | Authenticate using user credentials.                                    |
| AUTH-02 | Access Control | Restrict application features based on user roles and tenant ownership. |

## 7. Business Decisions

### 7.1 Quiz Structure

The application uses multiple quizzes.

Each quiz contains a collection of questions managed by the administrator.

This allows administrators to organize questions into separate assessments.

### 7.2 Changing Questions and Choices

Changes to questions and choices should not modify historical results.

The application uses a content-preservation approach:

* Previously submitted attempts retain their original question and answer information.
* Changes to questions or choices should not affect previously submitted results.
* Historical content is preserved through the implemented versioning or snapshot strategy.

### 7.3 Removing Questions

Questions are removed using soft deletion or an inactive status instead of physical deletion.

This ensures that:

* Removed questions are no longer available in new quizzes.
* Historical attempts and results remain accessible.
* Existing relationships are preserved.

### 7.4 Quiz Submissions and Retakes

Each quiz attempt represents an independent submission.

* Students can have multiple attempts if retakes are enabled.
* Previous submitted attempts are preserved.
* New attempts do not overwrite previous results.
* Only submitted attempts are included in final performance statistics.

### 7.5 Performance Statistics

Performance statistics are calculated using submitted attempts and their associated answers.

Examples:

* Student average score.
* Total attempts.
* Correct answer percentage per question.
* Most frequently incorrect questions.
* Student performance across quizzes.

Statistics use database-side aggregation to reduce unnecessary data retrieval.

## 8. Advanced Areas Implemented

The application implements two of the three advanced areas requested by the assignment.

### 8.1 Performance

**Problem Identified:**

Loading all student answers and calculating statistics in application memory can become inefficient as the number of students and attempts increases.

**Approach:**

* Use EF Core projections to retrieve only required fields.
* Use database-side aggregation such as Count, Sum, and Average.
* Apply AsNoTracking() for read-only queries.
* Add appropriate database indexes for frequently queried columns.
* Avoid unnecessary database calls and N+1 query problems.

**Why This Approach?**

The database is optimized for filtering and aggregation. Performing calculations at the database level reduces memory consumption and network traffic.

### 8.2 Reliability

**Problem Identified:**

Quiz submissions involve multiple database operations. A failure during submission could leave incomplete or inconsistent data.

Concurrent requests or repeated submissions may also result in duplicate or inconsistent attempts.

**Approach:**

Use database transactions together with optimistic concurrency control.

Quiz submission follows this sequence:

```text
Begin Transaction
       |
       v
Validate Attempt
       |
       v
Validate Submitted Answers
       |
       v
Save Student Answers
       |
       v
Calculate Score
       |
       v
Update Attempt Status
       |
       v
Commit Transaction
```

If any operation fails, the transaction is rolled back.

Additional reliability measures:

* Use optimistic concurrency with EF Core RowVersion where applicable.
* Prevent submitting an already submitted attempt.
* Validate that submitted answers belong to questions in the attempt.
* Ensure that the student owns the attempt.
* Enforce database constraints to protect data integrity.

**Why This Approach?**

Transactions ensure that related database operations succeed or fail together. Optimistic concurrency helps prevent conflicting updates, while submission validation protects against repeated or invalid requests.

## 9. Multi-Tenancy as an Additional Feature

Multi-tenancy is implemented as an additional architectural feature beyond the two selected advanced areas.

**Key characteristics:**

* Shared application infrastructure.
* Tenant-specific users and business data.
* Tenant-aware database queries.
* Tenant-based authorization.
* Protection against cross-tenant data access.

This allows multiple organizations to use the same LMS independently.

## 10. API Documentation

Swagger/OpenAPI is used to document and test the available API endpoints.

Main API groups:

* Authentication
* Admin Questions
* Admin Quizzes
* Admin Performance
* Student Quizzes
* Student Attempts
* Student Results

## 11. Database

The application uses SQL Server with Entity Framework Core.

Database changes are managed through EF Core Migrations.

Main entities include:

* Tenant
* User
* Quiz
* Question
* QuestionChoice
* QuizQuestion
* QuizAttempt
* QuizAnswer

Relationships and constraints are configured using Entity Framework Core configurations.

## 12. Testing

The application includes automated tests using xUnit.

Testing focuses on business rules, authorization, tenant isolation, and important edge cases.

Examples:

**Business Logic**

* Admin can create a question with valid choices.
* A question must have exactly one correct answer.
* Invalid answers are rejected.
* Students cannot submit an already submitted attempt.

**Authorization**

* Students cannot access admin endpoints.
* Students cannot access another student's results.

**Multi-Tenancy**

* Tenant A cannot access Tenant B's questions.
* Tenant A cannot update or delete Tenant B's data.
* Students cannot access quizzes or attempts from another tenant.

**Reliability**

* Failed submissions do not leave partial database changes.
* Concurrent submissions are handled correctly.
* Duplicate submissions are prevented.

Run tests using:

```bash
dotnet test
```

## 13. Running the Application

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio or VS Code

### 1. Clone the Repository

```bash
git clone <repository-url>
cd MiniLMS
```

### 2. Configure the Database

Update the connection string in appsettings.json or your development configuration.

### 3. Apply Migrations

```bash
dotnet ef migrations add ApplyDatabase --project Infrastructure\Infrastructure.csproj --startup-project API\API.csproj --output-dir Migrations
update-database
```

### 4. Run the Application

```bash
dotnet run --project API
```

### 5. Open Swagger

Navigate to the Swagger URL displayed when the API starts.
### 5. Postman Collection

A Postman Collection is provided to simplify API exploration and testing without requiring Swagger.

**How to Use:**

1. Import the provided Postman Collection.
2. Configure the API Base URL.
3. Use one of the following accounts to log in:

| Role    | Username  | Password      |
| ------- | --------- | ------------- |
| Admin   | `admin`   | `Aya@123#`    |
| Student | `student` | `Password123` |

4. Execute the Login request to obtain a JWT access token.
5. Use the token to access protected endpoints based on the user's role.

## 14. Future Improvements

* Background processing for notifications.
* Email notifications after quiz submission.
* Refresh token support.
* Advanced reporting and analytics.
* Caching frequently accessed quiz data.

## 15. Conclusion

Mini LMS demonstrates the implementation of a maintainable .NET Web API using Clean Architecture, common design patterns, centralized error handling, JWT authentication, database transactions, performance optimizations, automated testing, and multi-tenancy.

The application focuses on building a secure and reliable assessment platform while keeping business logic independent, testable, and extensible.
