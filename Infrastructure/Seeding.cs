using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Seeding
{
    public static class SeedData
    {
        // =========================================================
        // Common
        // =========================================================

        public static readonly DateTime SeedDate =
            new DateTime(
                2026,
                10,
                3,
                0,
                0,
                0,
                DateTimeKind.Utc);

        // =========================================================
        // TENANTS
        // =========================================================

        public static readonly Guid Tenant1Id =
            Guid.Parse("11111111-1111-1111-1111-111111111111");

        public static readonly Guid Tenant2Id =
            Guid.Parse("22222222-2222-2222-2222-222222222222");

        public static readonly Guid Tenant3Id =
            Guid.Parse("33333333-3333-3333-3333-333333333333");

        public static IEnumerable<Tenant> Tenants =>
        [
            new Tenant
            {
                Id = Tenant1Id,
                Name = "Tenant 1",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },

            new Tenant
            {
                Id = Tenant2Id,
                Name = "Tenant 2",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },

            new Tenant
            {
                Id = Tenant3Id,
                Name = "Tenant 3",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            }
        ];

        // =========================================================
        // USERS
        // =========================================================

        public static readonly Guid Admin1Id =
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        public static readonly Guid Student1Id =
            Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        public static readonly Guid Admin2Id =
            Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        public static readonly Guid Student2Id =
            Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

        public static readonly Guid Admin3Id =
            Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

        public static readonly Guid Student3Id =
            Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");

        public static IEnumerable<User> Users =>
        [
            // -----------------------------------------------------
            // Tenant 1
            // -----------------------------------------------------
            //Aya@123#
            new User
            {
                Id = Admin1Id,
                TenantId = Tenant1Id,
                Name = "admin",
                Email = "admin1@tenant1.com",
                PasswordHash =
                    "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            //Password123
            new User
            {
                Id = Student1Id,
                TenantId = Tenant1Id,
                Name = "student",
                Email = "student1@tenant1.com",
                PasswordHash = "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Tenant 2
            // -----------------------------------------------------
            //Aya@123#
            new User
            {
                Id = Admin2Id,
                TenantId = Tenant2Id,
                Name = "admin",
                Email = "admin2@tenant2.com",
                PasswordHash =
                   "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            //Password123
            new User
            {
                Id = Student2Id,
                TenantId = Tenant2Id,
                Name = "student",
                Email = "student2@tenant2.com",
                PasswordHash =
                    "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Tenant 3
            // -----------------------------------------------------
            //Aya@123#
            new User
            {
                Id = Admin3Id,
                TenantId = Tenant3Id,
                Name = "admin",
                Email = "admin3@tenant3.com",
                PasswordHash =
                    "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            //Password123
            new User
            {
                Id = Student3Id,
                TenantId = Tenant3Id,
                Name = "student",
                Email = "student3@tenant3.com",
                PasswordHash =
                    "MT6kNIiU0bP+QQQX4aWJIbIyXvYi5phudi0uwcmNAlIxMjM0",
                PasswordSalt = "1234",
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            }
        ];

        // =========================================================
        // QUESTIONS
        // =========================================================

        // Tenant 1
        public static readonly Guid Q1 =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        public static readonly Guid Q2 =
            Guid.Parse("10000000-0000-0000-0000-000000000002");

        public static readonly Guid Q3 =
            Guid.Parse("10000000-0000-0000-0000-000000000003");

        public static readonly Guid Q4 =
            Guid.Parse("10000000-0000-0000-0000-000000000004");

        public static readonly Guid Q5 =
            Guid.Parse("10000000-0000-0000-0000-000000000005");

        // Tenant 2
        public static readonly Guid Q6 =
            Guid.Parse("20000000-0000-0000-0000-000000000001");

        public static readonly Guid Q7 =
            Guid.Parse("20000000-0000-0000-0000-000000000002");

        public static readonly Guid Q8 =
            Guid.Parse("20000000-0000-0000-0000-000000000003");

        public static readonly Guid Q9 =
            Guid.Parse("20000000-0000-0000-0000-000000000004");

        public static readonly Guid Q10 =
            Guid.Parse("20000000-0000-0000-0000-000000000005");

        // Tenant 3
        public static readonly Guid Q11 =
            Guid.Parse("30000000-0000-0000-0000-000000000001");

        public static readonly Guid Q12 =
            Guid.Parse("30000000-0000-0000-0000-000000000002");

        public static readonly Guid Q13 =
            Guid.Parse("30000000-0000-0000-0000-000000000003");

        public static readonly Guid Q14 =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        public static readonly Guid Q15 =
            Guid.Parse("30000000-0000-0000-0000-000000000005");

        public static IEnumerable<Question> Questions =>
        [
            // =====================================================
            // Tenant 1 - C# / .NET
            // =====================================================

            new Question
            {
                Id = Q1,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Text = "What is C#?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q2,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Text = "What is Entity Framework Core?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q3,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Text = "Which keyword is used to create a class in C#?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q4,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Text = "What does API stand for?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q5,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Text = "What is Dependency Injection?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            // =====================================================
            // Tenant 2 - Database / Web
            // =====================================================

            new Question
            {
                Id = Q6,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Text = "What is LINQ?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q7,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Text = "What is a primary key?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q8,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Text = "What is a foreign key?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q9,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Text = "What is REST?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q10,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Text = "What is HTTP?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            // =====================================================
            // Tenant 3 - Advanced Backend
            // =====================================================

            new Question
            {
                Id = Q11,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Text = "What is SQL?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q12,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Text = "What is a database index?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q13,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Text = "What is a database transaction?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q14,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Text = "What is optimistic concurrency?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Question
            {
                Id = Q15,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Text = "What is a JWT?",
                ImageUrl = null,
                IsDeleted = false,
                IsLocked = false,
                PreviousQuestionId = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            }
        ];

        // =========================================================
        // QUESTION CHOICES
        // =========================================================

        public static IEnumerable<QuestionChoice> QuestionChoices =>
        [
            // -----------------------------------------------------
            // Q1
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("11000000-0000-0000-0000-000000000001"),
                QuestionId = Q1,
                Text = "A programming language",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("11000000-0000-0000-0000-000000000002"),
                QuestionId = Q1,
                Text = "A database",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("11000000-0000-0000-0000-000000000003"),
                QuestionId = Q1,
                Text = "An operating system",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("11000000-0000-0000-0000-000000000004"),
                QuestionId = Q1,
                Text = "A web server",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q2
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("12000000-0000-0000-0000-000000000001"),
                QuestionId = Q2,
                Text = "An ORM for .NET",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("12000000-0000-0000-0000-000000000002"),
                QuestionId = Q2,
                Text = "A JavaScript framework",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("12000000-0000-0000-0000-000000000003"),
                QuestionId = Q2,
                Text = "A database engine",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("12000000-0000-0000-0000-000000000004"),
                QuestionId = Q2,
                Text = "An operating system",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q3
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("13000000-0000-0000-0000-000000000001"),
                QuestionId = Q3,
                Text = "class",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("13000000-0000-0000-0000-000000000002"),
                QuestionId = Q3,
                Text = "object",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("13000000-0000-0000-0000-000000000003"),
                QuestionId = Q3,
                Text = "newclass",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("13000000-0000-0000-0000-000000000004"),
                QuestionId = Q3,
                Text = "type",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q4
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("14000000-0000-0000-0000-000000000001"),
                QuestionId = Q4,
                Text = "Application Programming Interface",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("14000000-0000-0000-0000-000000000002"),
                QuestionId = Q4,
                Text = "Application Program Internet",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("14000000-0000-0000-0000-000000000003"),
                QuestionId = Q4,
                Text = "Advanced Programming Interface",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("14000000-0000-0000-0000-000000000004"),
                QuestionId = Q4,
                Text = "Application Process Integration",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q5
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("15000000-0000-0000-0000-000000000001"),
                QuestionId = Q5,
                Text = "A design pattern for providing dependencies",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("15000000-0000-0000-0000-000000000002"),
                QuestionId = Q5,
                Text = "A database type",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("15000000-0000-0000-0000-000000000003"),
                QuestionId = Q5,
                Text = "An HTTP protocol",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("15000000-0000-0000-0000-000000000004"),
                QuestionId = Q5,
                Text = "A testing framework",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q6
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("21000000-0000-0000-0000-000000000001"),
                QuestionId = Q6,
                Text = "Language Integrated Query",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("21000000-0000-0000-0000-000000000002"),
                QuestionId = Q6,
                Text = "Language Internal Query",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("21000000-0000-0000-0000-000000000003"),
                QuestionId = Q6,
                Text = "Logical Integrated Query",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("21000000-0000-0000-0000-000000000004"),
                QuestionId = Q6,
                Text = "Local Integrated Query",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q7
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("22000000-0000-0000-0000-000000000001"),
                QuestionId = Q7,
                Text = "Uniquely identifies a row",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("22000000-0000-0000-0000-000000000002"),
                QuestionId = Q7,
                Text = "Stores duplicate rows",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("22000000-0000-0000-0000-000000000003"),
                QuestionId = Q7,
                Text = "Connects to an API",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("22000000-0000-0000-0000-000000000004"),
                QuestionId = Q7,
                Text = "Stores a password",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q8
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("23000000-0000-0000-0000-000000000001"),
                QuestionId = Q8,
                Text = "References a key in another table",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("23000000-0000-0000-0000-000000000002"),
                QuestionId = Q8,
                Text = "Always contains unique values",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("23000000-0000-0000-0000-000000000003"),
                QuestionId = Q8,
                Text = "Is always a primary key",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("23000000-0000-0000-0000-000000000004"),
                QuestionId = Q8,
                Text = "Stores encrypted data",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q9
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("24000000-0000-0000-0000-000000000001"),
                QuestionId = Q9,
                Text = "Representational State Transfer",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("24000000-0000-0000-0000-000000000002"),
                QuestionId = Q9,
                Text = "Remote State Transfer",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("24000000-0000-0000-0000-000000000003"),
                QuestionId = Q9,
                Text = "Resource State Transaction",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("24000000-0000-0000-0000-000000000004"),
                QuestionId = Q9,
                Text = "Remote Service Technology",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q10
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("25000000-0000-0000-0000-000000000001"),
                QuestionId = Q10,
                Text = "HyperText Transfer Protocol",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("25000000-0000-0000-0000-000000000002"),
                QuestionId = Q10,
                Text = "High Transfer Text Protocol",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("25000000-0000-0000-0000-000000000003"),
                QuestionId = Q10,
                Text = "Hyper Transfer Type Protocol",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("25000000-0000-0000-0000-000000000004"),
                QuestionId = Q10,
                Text = "Host Transfer Protocol",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q11
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("31000000-0000-0000-0000-000000000001"),
                QuestionId = Q11,
                Text = "Structured Query Language",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("31000000-0000-0000-0000-000000000002"),
                QuestionId = Q11,
                Text = "Simple Query Language",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("31000000-0000-0000-0000-000000000003"),
                QuestionId = Q11,
                Text = "System Query Language",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("31000000-0000-0000-0000-000000000004"),
                QuestionId = Q11,
                Text = "Server Query Logic",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q12
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("32000000-0000-0000-0000-000000000001"),
                QuestionId = Q12,
                Text = "A data structure that improves query performance",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("32000000-0000-0000-0000-000000000002"),
                QuestionId = Q12,
                Text = "A database backup",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("32000000-0000-0000-0000-000000000003"),
                QuestionId = Q12,
                Text = "A database user",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("32000000-0000-0000-0000-000000000004"),
                QuestionId = Q12,
                Text = "A stored password",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q13
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("33000000-0000-0000-0000-000000000001"),
                QuestionId = Q13,
                Text = "A group of operations treated as one unit",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("33000000-0000-0000-0000-000000000002"),
                QuestionId = Q13,
                Text = "A database table",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("33000000-0000-0000-0000-000000000003"),
                QuestionId = Q13,
                Text = "A database index",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("33000000-0000-0000-0000-000000000004"),
                QuestionId = Q13,
                Text = "A user account",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q14
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("34000000-0000-0000-0000-000000000001"),
                QuestionId = Q14,
                Text = "Detecting changes using a version value",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("34000000-0000-0000-0000-000000000002"),
                QuestionId = Q14,
                Text = "Locking every database row",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("34000000-0000-0000-0000-000000000003"),
                QuestionId = Q14,
                Text = "Deleting duplicate records",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("34000000-0000-0000-0000-000000000004"),
                QuestionId = Q14,
                Text = "Encrypting database columns",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            // -----------------------------------------------------
            // Q15
            // -----------------------------------------------------

            new QuestionChoice
            {
                Id = Guid.Parse("35000000-0000-0000-0000-000000000001"),
                QuestionId = Q15,
                Text = "JSON Web Token",
                IsCorrect = true,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("35000000-0000-0000-0000-000000000002"),
                QuestionId = Q15,
                Text = "Java Web Transfer",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("35000000-0000-0000-0000-000000000003"),
                QuestionId = Q15,
                Text = "JavaScript Web Token",
                IsCorrect = false,
                CreatedAt = SeedDate
            },

            new QuestionChoice
            {
                Id = Guid.Parse("35000000-0000-0000-0000-000000000004"),
                QuestionId = Q15,
                Text = "JSON Web Transfer",
                IsCorrect = false,
                CreatedAt = SeedDate
            }
        ];

        // =========================================================
        // QUIZZES
        // =========================================================

        public static readonly Guid Quiz1 =
            Guid.Parse("40000000-0000-0000-0000-000000000001");

        public static readonly Guid Quiz2 =
            Guid.Parse("40000000-0000-0000-0000-000000000002");

        public static readonly Guid Quiz3 =
            Guid.Parse("40000000-0000-0000-0000-000000000003");

        public static readonly Guid Quiz4 =
            Guid.Parse("40000000-0000-0000-0000-000000000004");

        public static readonly Guid Quiz5 =
            Guid.Parse("40000000-0000-0000-0000-000000000005");

        public static readonly Guid Quiz6 =
            Guid.Parse("40000000-0000-0000-0000-000000000006");

        public static IEnumerable<Quiz> Quizzes =>
        [
            new Quiz
            {
                Id = Quiz1,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Title = "C# Fundamentals",
                Description = "Basic C# and .NET knowledge.",
                IsActive = true,
                MaxAttempts = 3,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Quiz
            {
                Id = Quiz2,
                TenantId = Tenant1Id,
                CreatedByUserId = Admin1Id,
                Title = "Backend Fundamentals",
                Description = "Backend development concepts.",
                IsActive = true,
                MaxAttempts = 2,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Quiz
            {
                Id = Quiz3,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Title = "Database Fundamentals",
                Description = "SQL and relational database concepts.",
                IsActive = true,
                MaxAttempts = 3,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Quiz
            {
                Id = Quiz4,
                TenantId = Tenant2Id,
                CreatedByUserId = Admin2Id,
                Title = "Web Fundamentals",
                Description = "HTTP and REST concepts.",
                IsActive = true,
                MaxAttempts = null,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Quiz
            {
                Id = Quiz5,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Title = "Advanced Backend",
                Description = "Transactions and concurrency.",
                IsActive = true,
                MaxAttempts = 2,
                CreatedAt = SeedDate,
                UpdatedAt = null
            },

            new Quiz
            {
                Id = Quiz6,
                TenantId = Tenant3Id,
                CreatedByUserId = Admin3Id,
                Title = "Authentication",
                Description = "Authentication and JWT concepts.",
                IsActive = true,
                MaxAttempts = 3,
                CreatedAt = SeedDate,
                UpdatedAt = null
            }
        ];

        // =========================================================
        // QUIZ QUESTIONS
        // =========================================================

        public static IEnumerable<QuizQuestion> QuizQuestions =>
        [
            // Quiz 1
            new QuizQuestion
            {
                QuizId = Quiz1,
                QuestionId = Q1,
                DisplayOrder = 1
            },

            new QuizQuestion
            {
                QuizId = Quiz1,
                QuestionId = Q2,
                DisplayOrder = 2
            },

            new QuizQuestion
            {
                QuizId = Quiz1,
                QuestionId = Q3,
                DisplayOrder = 3
            },

            new QuizQuestion
            {
                QuizId = Quiz1,
                QuestionId = Q4,
                DisplayOrder = 4
            },

            new QuizQuestion
            {
                QuizId = Quiz1,
                QuestionId = Q5,
                DisplayOrder = 5
            },

            // Quiz 2
            new QuizQuestion
            {
                QuizId = Quiz2,
                QuestionId = Q2,
                DisplayOrder = 1
            },

            new QuizQuestion
            {
                QuizId = Quiz2,
                QuestionId = Q4,
                DisplayOrder = 2
            },

            new QuizQuestion
            {
                QuizId = Quiz2,
                QuestionId = Q5,
                DisplayOrder = 3
            },

            // Quiz 3
            new QuizQuestion
            {
                QuizId = Quiz3,
                QuestionId = Q6,
                DisplayOrder = 1
            },

            new QuizQuestion
            {
                QuizId = Quiz3,
                QuestionId = Q7,
                DisplayOrder = 2
            },

            new QuizQuestion
            {
                QuizId = Quiz3,
                QuestionId = Q8,
                DisplayOrder = 3
            },

            // Quiz 4
            new QuizQuestion
            {
                QuizId = Quiz4,
                QuestionId = Q9,
                DisplayOrder = 1
            },

            new QuizQuestion
            {
                QuizId = Quiz4,
                QuestionId = Q10,
                DisplayOrder = 2
            },

            // Quiz 5
            new QuizQuestion
            {
                QuizId = Quiz5,
                QuestionId = Q11,
                DisplayOrder = 1
            },

            new QuizQuestion
            {
                QuizId = Quiz5,
                QuestionId = Q12,
                DisplayOrder = 2
            },

            new QuizQuestion
            {
                QuizId = Quiz5,
                QuestionId = Q13,
                DisplayOrder = 3
            },

            new QuizQuestion
            {
                QuizId = Quiz5,
                QuestionId = Q14,
                DisplayOrder = 4
            },

            // Quiz 6
            new QuizQuestion
            {
                QuizId = Quiz6,
                QuestionId = Q15,
                DisplayOrder = 1
            }
        ];

        // =========================================================
        // QUIZ ATTEMPTS
        // =========================================================

        public static readonly Guid Attempt1 =
            Guid.Parse("50000000-0000-0000-0000-000000000001");

        public static readonly Guid Attempt2 =
            Guid.Parse("50000000-0000-0000-0000-000000000002");

        public static readonly Guid Attempt3 =
            Guid.Parse("50000000-0000-0000-0000-000000000003");

        public static readonly Guid Attempt4 =
            Guid.Parse("50000000-0000-0000-0000-000000000004");

        public static readonly Guid Attempt5 =
            Guid.Parse("50000000-0000-0000-0000-000000000005");

        public static readonly Guid Attempt6 =
            Guid.Parse("50000000-0000-0000-0000-000000000006");

        public static IEnumerable<QuizAttempt> QuizAttempts =>
        [
            // Tenant 1 - Student 1 - Quiz 1
            new QuizAttempt
            {
                Id = Attempt1,
                TenantId = Tenant1Id,
                QuizId = Quiz1,
                StudentId = Student1Id,
                Status = AttemptStatus.Submitted,
                Score = 4,
                TotalQuestions = 5,
                Percentage = 80,
                StartedAt = SeedDate.AddHours(1),
                SubmittedAt = SeedDate.AddHours(1).AddMinutes(20)
            },

            // Second attempt
            new QuizAttempt
            {
                Id = Attempt2,
                TenantId = Tenant1Id,
                QuizId = Quiz1,
                StudentId = Student1Id,
                Status = AttemptStatus.Submitted,
                Score = 5,
                TotalQuestions = 5,
                Percentage = 100,
                StartedAt = SeedDate.AddHours(2),
                SubmittedAt = SeedDate.AddHours(2).AddMinutes(15)
            },

            // Tenant 1 - another quiz
            new QuizAttempt
            {
                Id = Attempt3,
                TenantId = Tenant1Id,
                QuizId = Quiz2,
                StudentId = Student1Id,
                Status = AttemptStatus.InProgress,
                Score = 0,
                TotalQuestions = 3,
                Percentage = 0,
                StartedAt = SeedDate.AddHours(3),
                SubmittedAt = null
            },

            // Tenant 2
            new QuizAttempt
            {
                Id = Attempt4,
                TenantId = Tenant2Id,
                QuizId = Quiz3,
                StudentId = Student2Id,
                Status = AttemptStatus.Submitted,
                Score = 2,
                TotalQuestions = 3,
                Percentage = 66.67m,
                StartedAt = SeedDate.AddHours(1),
                SubmittedAt = SeedDate.AddHours(1).AddMinutes(25)
            },

            // Tenant 3
            new QuizAttempt
            {
                Id = Attempt5,
                TenantId = Tenant3Id,
                QuizId = Quiz5,
                StudentId = Student3Id,
                Status = AttemptStatus.Submitted,
                Score = 3,
                TotalQuestions = 4,
                Percentage = 75,
                StartedAt = SeedDate.AddHours(2),
                SubmittedAt = SeedDate.AddHours(2).AddMinutes(30)
            },

            // Tenant 3 - another quiz
            new QuizAttempt
            {
                Id = Attempt6,
                TenantId = Tenant3Id,
                QuizId = Quiz6,
                StudentId = Student3Id,
                Status = AttemptStatus.Submitted,
                Score = 1,
                TotalQuestions = 1,
                Percentage = 100,
                StartedAt = SeedDate.AddHours(4),
                SubmittedAt = SeedDate.AddHours(4).AddMinutes(10)
            }
        ];

        // =========================================================
        // QUIZ ANSWERS
        // =========================================================

        public static IEnumerable<QuizAnswer> QuizAnswers =>
        [
            // =====================================================
            // Attempt 1 - 4/5
            // =====================================================

            new QuizAnswer
            {
                Id = Guid.Parse("51000000-0000-0000-0000-000000000001"),
                AttemptId = Attempt1,
                QuestionId = Q1,
                SelectedChoiceId =
                    Guid.Parse("11000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(5)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("51000000-0000-0000-0000-000000000002"),
                AttemptId = Attempt1,
                QuestionId = Q2,
                SelectedChoiceId =
                    Guid.Parse("12000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(6)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("51000000-0000-0000-0000-000000000003"),
                AttemptId = Attempt1,
                QuestionId = Q3,
                SelectedChoiceId =
                    Guid.Parse("13000000-0000-0000-0000-000000000002"),
                IsCorrect = false,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(7)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("51000000-0000-0000-0000-000000000004"),
                AttemptId = Attempt1,
                QuestionId = Q4,
                SelectedChoiceId =
                    Guid.Parse("14000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(8)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("51000000-0000-0000-0000-000000000005"),
                AttemptId = Attempt1,
                QuestionId = Q5,
                SelectedChoiceId =
                    Guid.Parse("15000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(9)
            },

            // =====================================================
            // Attempt 2 - 5/5
            // =====================================================

            new QuizAnswer
            {
                Id = Guid.Parse("52000000-0000-0000-0000-000000000001"),
                AttemptId = Attempt2,
                QuestionId = Q1,
                SelectedChoiceId =
                    Guid.Parse("11000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(5)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("52000000-0000-0000-0000-000000000002"),
                AttemptId = Attempt2,
                QuestionId = Q2,
                SelectedChoiceId =
                    Guid.Parse("12000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(6)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("52000000-0000-0000-0000-000000000003"),
                AttemptId = Attempt2,
                QuestionId = Q3,
                SelectedChoiceId =
                    Guid.Parse("13000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(7)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("52000000-0000-0000-0000-000000000004"),
                AttemptId = Attempt2,
                QuestionId = Q4,
                SelectedChoiceId =
                    Guid.Parse("14000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(8)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("52000000-0000-0000-0000-000000000005"),
                AttemptId = Attempt2,
                QuestionId = Q5,
                SelectedChoiceId =
                    Guid.Parse("15000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(9)
            },

            // =====================================================
            // Attempt 4 - Tenant 2 - 2/3
            // =====================================================

            new QuizAnswer
            {
                Id = Guid.Parse("54000000-0000-0000-0000-000000000001"),
                AttemptId = Attempt4,
                QuestionId = Q6,
                SelectedChoiceId =
                    Guid.Parse("21000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(5)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("54000000-0000-0000-0000-000000000002"),
                AttemptId = Attempt4,
                QuestionId = Q7,
                SelectedChoiceId =
                    Guid.Parse("22000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(6)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("54000000-0000-0000-0000-000000000003"),
                AttemptId = Attempt4,
                QuestionId = Q8,
                SelectedChoiceId =
                    Guid.Parse("23000000-0000-0000-0000-000000000002"),
                IsCorrect = false,
                CreatedAt = SeedDate.AddHours(1).AddMinutes(7)
            },

            // =====================================================
            // Attempt 5 - Tenant 3 - 3/4
            // =====================================================

            new QuizAnswer
            {
                Id = Guid.Parse("55000000-0000-0000-0000-000000000001"),
                AttemptId = Attempt5,
                QuestionId = Q11,
                SelectedChoiceId =
                    Guid.Parse("31000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(5)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("55000000-0000-0000-0000-000000000002"),
                AttemptId = Attempt5,
                QuestionId = Q12,
                SelectedChoiceId =
                    Guid.Parse("32000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(6)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("55000000-0000-0000-0000-000000000003"),
                AttemptId = Attempt5,
                QuestionId = Q13,
                SelectedChoiceId =
                    Guid.Parse("33000000-0000-0000-0000-000000000002"),
                IsCorrect = false,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(7)
            },

            new QuizAnswer
            {
                Id = Guid.Parse("55000000-0000-0000-0000-000000000004"),
                AttemptId = Attempt5,
                QuestionId = Q14,
                SelectedChoiceId =
                    Guid.Parse("34000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(2).AddMinutes(8)
            },

            // =====================================================
            // Attempt 6 - Tenant 3 - 1/1
            // =====================================================

            new QuizAnswer
            {
                Id = Guid.Parse("56000000-0000-0000-0000-000000000001"),
                AttemptId = Attempt6,
                QuestionId = Q15,
                SelectedChoiceId =
                    Guid.Parse("35000000-0000-0000-0000-000000000001"),
                IsCorrect = true,
                CreatedAt = SeedDate.AddHours(4).AddMinutes(5)
            }
        ];
    }
}