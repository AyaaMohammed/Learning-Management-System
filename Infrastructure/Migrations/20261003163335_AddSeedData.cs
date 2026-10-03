using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PreviousQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MaxAttempts = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quiz_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionChoice_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 0m),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttempt_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizAttempt_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizAttempt_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswer_QuestionChoice_SelectedChoiceId",
                        column: x => x.SelectedChoiceId,
                        principalTable: "QuestionChoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizAnswer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizAnswer_QuizAttempt_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "QuizAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Tenant 1", new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Tenant 2", new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Tenant 3", new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PasswordSalt", "Role", "TenantId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "admin1@tenant1.com", true, "admin", "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0", "1234", 1, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "student1@tenant1.com", true, "student", "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0", "1234", 2, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "admin2@tenant2.com", true, "admin", "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0", "1234", 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "student2@tenant2.com", true, "student", "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0", "1234", 2, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "admin3@tenant3.com", true, "admin", "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0", "1234", 1, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "student3@tenant3.com", true, "student", "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0", "1234", 2, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "ImageUrl", "PreviousQuestionId", "TenantId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, null, new Guid("11111111-1111-1111-1111-111111111111"), "What is C#?", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, null, new Guid("11111111-1111-1111-1111-111111111111"), "What is Entity Framework Core?", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, null, new Guid("11111111-1111-1111-1111-111111111111"), "Which keyword is used to create a class in C#?", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, null, new Guid("11111111-1111-1111-1111-111111111111"), "What does API stand for?", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, null, new Guid("11111111-1111-1111-1111-111111111111"), "What is Dependency Injection?", null },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, null, new Guid("22222222-2222-2222-2222-222222222222"), "What is LINQ?", null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, null, new Guid("22222222-2222-2222-2222-222222222222"), "What is a primary key?", null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, null, new Guid("22222222-2222-2222-2222-222222222222"), "What is a foreign key?", null },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, null, new Guid("22222222-2222-2222-2222-222222222222"), "What is REST?", null },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, null, new Guid("22222222-2222-2222-2222-222222222222"), "What is HTTP?", null },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, null, new Guid("33333333-3333-3333-3333-333333333333"), "What is SQL?", null },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, null, new Guid("33333333-3333-3333-3333-333333333333"), "What is a database index?", null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, null, new Guid("33333333-3333-3333-3333-333333333333"), "What is a database transaction?", null },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, null, new Guid("33333333-3333-3333-3333-333333333333"), "What is optimistic concurrency?", null },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, null, new Guid("33333333-3333-3333-3333-333333333333"), "What is a JWT?", null }
                });

            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "IsActive", "MaxAttempts", "TenantId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Basic C# and .NET knowledge.", true, 3, new Guid("11111111-1111-1111-1111-111111111111"), "C# Fundamentals", null },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Backend development concepts.", true, 2, new Guid("11111111-1111-1111-1111-111111111111"), "Backend Fundamentals", null },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "SQL and relational database concepts.", true, 3, new Guid("22222222-2222-2222-2222-222222222222"), "Database Fundamentals", null },
                    { new Guid("40000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "HTTP and REST concepts.", true, null, new Guid("22222222-2222-2222-2222-222222222222"), "Web Fundamentals", null },
                    { new Guid("40000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Transactions and concurrency.", true, 2, new Guid("33333333-3333-3333-3333-333333333333"), "Advanced Backend", null },
                    { new Guid("40000000-0000-0000-0000-000000000006"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Authentication and JWT concepts.", true, 3, new Guid("33333333-3333-3333-3333-333333333333"), "Authentication", null }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("11000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000001"), "A programming language" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("11000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "A database" },
                    { new Guid("11000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "An operating system" },
                    { new Guid("11000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "A web server" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("12000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000002"), "An ORM for .NET" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("12000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), "A JavaScript framework" },
                    { new Guid("12000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), "A database engine" },
                    { new Guid("12000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), "An operating system" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("13000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000003"), "class" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("13000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), "object" },
                    { new Guid("13000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), "newclass" },
                    { new Guid("13000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), "type" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("14000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000004"), "Application Programming Interface" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("14000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), "Application Program Internet" },
                    { new Guid("14000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), "Advanced Programming Interface" },
                    { new Guid("14000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), "Application Process Integration" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("15000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000005"), "A design pattern for providing dependencies" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("15000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000005"), "A database type" },
                    { new Guid("15000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000005"), "An HTTP protocol" },
                    { new Guid("15000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000005"), "A testing framework" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("21000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000001"), "Language Integrated Query" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("21000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "Language Internal Query" },
                    { new Guid("21000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "Logical Integrated Query" },
                    { new Guid("21000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "Local Integrated Query" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("22000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000002"), "Uniquely identifies a row" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("22000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), "Stores duplicate rows" },
                    { new Guid("22000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), "Connects to an API" },
                    { new Guid("22000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), "Stores a password" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("23000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000003"), "References a key in another table" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("23000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), "Always contains unique values" },
                    { new Guid("23000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), "Is always a primary key" },
                    { new Guid("23000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), "Stores encrypted data" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("24000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000004"), "Representational State Transfer" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("24000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), "Remote State Transfer" },
                    { new Guid("24000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), "Resource State Transaction" },
                    { new Guid("24000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), "Remote Service Technology" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("25000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000005"), "HyperText Transfer Protocol" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("25000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), "High Transfer Text Protocol" },
                    { new Guid("25000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), "Hyper Transfer Type Protocol" },
                    { new Guid("25000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), "Host Transfer Protocol" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("31000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000001"), "Structured Query Language" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("31000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), "Simple Query Language" },
                    { new Guid("31000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), "System Query Language" },
                    { new Guid("31000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), "Server Query Logic" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("32000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000002"), "A data structure that improves query performance" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("32000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), "A database backup" },
                    { new Guid("32000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), "A database user" },
                    { new Guid("32000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), "A stored password" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("33000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000003"), "A group of operations treated as one unit" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("33000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), "A database table" },
                    { new Guid("33000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), "A database index" },
                    { new Guid("33000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), "A user account" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("34000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000004"), "Detecting changes using a version value" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("34000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), "Locking every database row" },
                    { new Guid("34000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), "Deleting duplicate records" },
                    { new Guid("34000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), "Encrypting database columns" }
                });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("35000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000005"), "JSON Web Token" });

            migrationBuilder.InsertData(
                table: "QuestionChoice",
                columns: new[] { "Id", "CreatedAt", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("35000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), "Java Web Transfer" },
                    { new Guid("35000000-0000-0000-0000-000000000003"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), "JavaScript Web Token" },
                    { new Guid("35000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), "JSON Web Transfer" }
                });

            migrationBuilder.InsertData(
                table: "QuizAttempt",
                columns: new[] { "Id", "Percentage", "QuizId", "Score", "StartedAt", "Status", "StudentId", "SubmittedAt", "TenantId", "TotalQuestions" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 80m, new Guid("40000000-0000-0000-0000-000000000001"), 4, new DateTime(2026, 10, 3, 1, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 10, 3, 1, 20, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), 5 },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 100m, new Guid("40000000-0000-0000-0000-000000000001"), 5, new DateTime(2026, 10, 3, 2, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 10, 3, 2, 15, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), 5 }
                });

            migrationBuilder.InsertData(
                table: "QuizAttempt",
                columns: new[] { "Id", "QuizId", "StartedAt", "Status", "StudentId", "SubmittedAt", "TenantId", "TotalQuestions" },
                values: new object[] { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 3, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new Guid("11111111-1111-1111-1111-111111111111"), 3 });

            migrationBuilder.InsertData(
                table: "QuizAttempt",
                columns: new[] { "Id", "Percentage", "QuizId", "Score", "StartedAt", "Status", "StudentId", "SubmittedAt", "TenantId", "TotalQuestions" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000004"), 66.67m, new Guid("40000000-0000-0000-0000-000000000003"), 2, new DateTime(2026, 10, 3, 1, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 10, 3, 1, 25, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222"), 3 },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 75m, new Guid("40000000-0000-0000-0000-000000000005"), 3, new DateTime(2026, 10, 3, 2, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 10, 3, 2, 30, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333"), 4 },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 100m, new Guid("40000000-0000-0000-0000-000000000006"), 1, new DateTime(2026, 10, 3, 4, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 10, 3, 4, 10, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333"), 1 }
                });

            migrationBuilder.InsertData(
                table: "QuizQuestion",
                columns: new[] { "QuestionId", "QuizId", "DisplayOrder" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), 1 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001"), 2 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001"), 3 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000001"), 4 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000001"), 5 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002"), 1 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002"), 2 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000002"), 3 },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000003"), 1 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000003"), 2 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000003"), 3 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004"), 1 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000004"), 2 },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000005"), 1 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000005"), 2 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000005"), 3 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000005"), 4 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000006"), 1 }
                });

            migrationBuilder.InsertData(
                table: "QuizAnswer",
                columns: new[] { "Id", "AttemptId", "CreatedAt", "IsCorrect", "QuestionId", "SelectedChoiceId" },
                values: new object[,]
                {
                    { new Guid("51000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 1, 5, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11000000-0000-0000-0000-000000000001") },
                    { new Guid("51000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 1, 6, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000002"), new Guid("12000000-0000-0000-0000-000000000001") },
                    { new Guid("51000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 1, 7, 0, 0, DateTimeKind.Utc), false, new Guid("10000000-0000-0000-0000-000000000003"), new Guid("13000000-0000-0000-0000-000000000002") },
                    { new Guid("51000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 1, 8, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000004"), new Guid("14000000-0000-0000-0000-000000000001") },
                    { new Guid("51000000-0000-0000-0000-000000000005"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 10, 3, 1, 9, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000005"), new Guid("15000000-0000-0000-0000-000000000001") },
                    { new Guid("52000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 2, 5, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11000000-0000-0000-0000-000000000001") },
                    { new Guid("52000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 2, 6, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000002"), new Guid("12000000-0000-0000-0000-000000000001") },
                    { new Guid("52000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 2, 7, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000003"), new Guid("13000000-0000-0000-0000-000000000001") },
                    { new Guid("52000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 2, 8, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000004"), new Guid("14000000-0000-0000-0000-000000000001") },
                    { new Guid("52000000-0000-0000-0000-000000000005"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 10, 3, 2, 9, 0, 0, DateTimeKind.Utc), true, new Guid("10000000-0000-0000-0000-000000000005"), new Guid("15000000-0000-0000-0000-000000000001") },
                    { new Guid("54000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 1, 5, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000001"), new Guid("21000000-0000-0000-0000-000000000001") },
                    { new Guid("54000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 1, 6, 0, 0, DateTimeKind.Utc), true, new Guid("20000000-0000-0000-0000-000000000002"), new Guid("22000000-0000-0000-0000-000000000001") },
                    { new Guid("54000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2026, 10, 3, 1, 7, 0, 0, DateTimeKind.Utc), false, new Guid("20000000-0000-0000-0000-000000000003"), new Guid("23000000-0000-0000-0000-000000000002") },
                    { new Guid("55000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 2, 5, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000001"), new Guid("31000000-0000-0000-0000-000000000001") },
                    { new Guid("55000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 2, 6, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000002"), new Guid("32000000-0000-0000-0000-000000000001") },
                    { new Guid("55000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 2, 7, 0, 0, DateTimeKind.Utc), false, new Guid("30000000-0000-0000-0000-000000000003"), new Guid("33000000-0000-0000-0000-000000000002") },
                    { new Guid("55000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 3, 2, 8, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000004"), new Guid("34000000-0000-0000-0000-000000000001") },
                    { new Guid("56000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(2026, 10, 3, 4, 5, 0, 0, DateTimeKind.Utc), true, new Guid("30000000-0000-0000-0000-000000000005"), new Guid("35000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_CreatedByUserId",
                table: "Question",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TenantId",
                table: "Question",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TenantId_IsDeleted",
                table: "Question",
                columns: new[] { "TenantId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoice_QuestionId",
                table: "QuestionChoice",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_CreatedByUserId",
                table: "Quiz",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_TenantId",
                table: "Quiz",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_TenantId_IsActive",
                table: "Quiz",
                columns: new[] { "TenantId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswer_AttemptId_QuestionId",
                table: "QuizAnswer",
                columns: new[] { "AttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswer_QuestionId",
                table: "QuizAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswer_SelectedChoiceId",
                table: "QuizAnswer",
                column: "SelectedChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_QuizId",
                table: "QuizAttempt",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_StudentId_QuizId",
                table: "QuizAttempt",
                columns: new[] { "StudentId", "QuizId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_SubmittedAt",
                table: "QuizAttempt",
                column: "SubmittedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_TenantId_QuizId",
                table: "QuizAttempt",
                columns: new[] { "TenantId", "QuizId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_TenantId_StudentId",
                table: "QuizAttempt",
                columns: new[] { "TenantId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId_Email",
                table: "User",
                columns: new[] { "TenantId", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAnswer");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "QuestionChoice");

            migrationBuilder.DropTable(
                name: "QuizAttempt");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
