using DALTestsDB.Configurations;
using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TestLib;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Bodies;
using TestLib.Classes.Enums;
using TestLib.Classes.Network;
using TestLib.Classes.Tasks;
using TestLib.Classes.Test;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB
{
    public class TestDBContext : DbContext
    {
        public DbSet<Test> Test { get; set; }

        public DbSet<Task> Task { get; set; }
        public DbSet<ChooseFromListTask> ChooseFromListTask { get; set; }
        public DbSet<MatchTask> MatchTask { get; set; }
        public DbSet<EnterTextTask> EnterTextTask { get; set; }

        public DbSet<Body> Body { get; set; }
        public DbSet<TextBody> TextBody { get; set; }
        public DbSet<ImageBody> ImageBody { get; set; }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<TextAnswer> TextAnswer { get; set; }
        public DbSet<ImageAnswer> ImageAnswer { get; set; }
        public DbSet<MatchAnswer> MatchAnswer { get; set; }
        public DbSet<EnterTextAnswer> EnterTextAnswer { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<UserTestResult> UserTestResult { get; set; }
        public DbSet<UserTaskResult> UserTaskResults { get; set; }
        public DbSet<UserAnswerResult> UserAnswerResults { get; set; }

        public DbSet<TestAssigned> TestAssigned { get; set; }
        public DbSet<TestAssignedUser> TestAssignedUser { get; set; }

        private static bool _created = false;
        public TestDBContext()
        {

        }
        public TestDBContext(DbContextOptions options) : base(options)
        {
            if (/*IsDatabaseCreated() == false*/ _created == false)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
                var matchAnswers = new MatchAnswer[]
                {
                    new () { Text = "Image", IsCorrect = true, TaskId = 3, Side = MatchSide.Left },
                    new () { Text = "Picture", IsCorrect = true, TaskId = 3, Side = MatchSide.Right },
                    new () { Text = "Apartment", IsCorrect = true, TaskId = 3, Side = MatchSide.Left},
                    new () { Text = "Flat", IsCorrect = true, TaskId = 3, Side = MatchSide.Right},
                    new () { Text = "pants", IsCorrect = true, TaskId = 3, Side = MatchSide.Left },
                    new () { Text = "trousers", IsCorrect = true, TaskId = 3, Side = MatchSide.Right },
                };
                MatchAnswer!.AddRange(matchAnswers);
                SaveChanges();

                matchAnswers[0].PartnerId = matchAnswers[1].Id;
                matchAnswers[1].PartnerId = matchAnswers[0].Id;
                matchAnswers[2].PartnerId = matchAnswers[3].Id;
                matchAnswers[3].PartnerId = matchAnswers[2].Id;
                matchAnswers[4].PartnerId = matchAnswers[5].Id;
                matchAnswers[5].PartnerId = matchAnswers[4].Id;


                SaveChanges();
            }
        }

        public bool IsDatabaseCreated()
        {
            return Database.CanConnect();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Group>(new GroupConfiguration());
            modelBuilder.ApplyConfiguration<UserGroup>(new UserGroupConfiguration());
            modelBuilder.ApplyConfiguration<Test>(new TestConfiguration());
            modelBuilder.ApplyConfiguration<TestAssigned>(new TestAssignedConfiguration());
            modelBuilder.ApplyConfiguration<UserTestResult>(new UserTestResultConfiguration());
            modelBuilder.ApplyConfiguration<UserTaskResult>(new UserTaskResultConfiguration());
            modelBuilder.ApplyConfiguration<UserAnswerResult>(new UserAnswerResultConfiguration());
            modelBuilder.ApplyConfiguration<Task>(new TaskConfiguration());
            modelBuilder.ApplyConfiguration<Body>(new BodyConfiguration());
            modelBuilder.ApplyConfiguration<Answer>(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration<MatchAnswer>(new MatchAnswerConfiguration());




            IEncryptor encryptor = new Sha256Encryptor();
            var users = new User[]
            {
                new (){Id = 1, FirstName = "Admin", LastName = "Admin", Login = "admin", Password = encryptor.Encrypt("admin"), Role = UserRole.Admin, CreatedAt = new (2000,02,15), IsArchived = false },
                new (){Id = 2, FirstName = "User", LastName = "User", Login = "user", Password = encryptor.Encrypt("user"), Role = UserRole.User, CreatedAt = new (2001,01,12), IsArchived = false },
                new (){Id = 3, FirstName = "User1", LastName = "User1", Login = "user1", Password = encryptor.Encrypt("user1"), Role = UserRole.User, CreatedAt = new (2002,03,11), IsArchived = true},
                new (){Id = 4, FirstName = "User2", LastName = "User2", Login = "user2", Password = encryptor.Encrypt("user2"), Role = UserRole.User, CreatedAt = new (2005,05,21), IsArchived = false}
            };
            var groups = new Group[]
            {
                new (){Id = 1, Name = "Admins", Description = "Admins group"},
                new (){Id = 2, Name = "Users", Description = "Users group"},
            };
            var userGroups = new UserGroup[]
            {
                new (){UserId = 1, GroupId = 1},
                new (){UserId = 2, GroupId = 2},
                new (){UserId = 3, GroupId = 2},
                new (){UserId = 4, GroupId = 2,}
            };


            var textAnswer = new TextAnswer[]
            {
                new (){Id = 1, Text = "1", IsCorrect = false, TaskId = 1,},
                new (){Id = 2, Text = "2", IsCorrect = false, TaskId = 1},
                new (){Id = 3, Text = "3", IsCorrect = false, TaskId = 1},
                new (){Id = 4, Text = "4", IsCorrect = true, TaskId = 1,},

                new (){Id = 5, Text = "1", IsCorrect = false, TaskId = 2,},
                new (){Id = 6, Text = "2", IsCorrect = false, TaskId = 2},
                new (){Id = 7, Text = "3", IsCorrect = false, TaskId = 2},
                new (){Id = 8, Text = "4", IsCorrect = true, TaskId = 2,},
            };
            var imageAnswer = new ImageAnswer[]
            {
                new (){Id = 9, ImagePath = "/bird.png", ImageLength = 20_657, IsCorrect = true, TaskId = 4,},
                new (){Id = 10, ImagePath = "/cat.png", ImageLength = 38_024, IsCorrect = false, TaskId = 4,},
                new (){Id = 11, ImagePath = "/dog.png", ImageLength = 38_439, IsCorrect = false, TaskId = 4,},
                new (){Id = 12, ImagePath = "/clown-fish.png", ImageLength = 30_455, IsCorrect = false, TaskId = 4,},
            };
            var enterTextAnswer = new EnterTextAnswer[]
            {
                new (){Id = 13, Text = "1", IsCorrect = false, TaskId = 5,},
                new (){Id = 14, Text = "2", IsCorrect = false, TaskId = 5},
                new (){Id = 15, Text = "3", IsCorrect = false, TaskId = 5},
                new (){Id = 16, Text = "4", IsCorrect = true, TaskId = 5},
            };


            var imageBodies = new ImageBody[]
            {
                new (){Id = 2, ImagePath = "/number-4.png", ImageLength = 27_648, TaskId = 2, Text = "Select the number on the picture"},
                new (){Id = 4, ImagePath = "/forest.png", ImageLength = 24_567, TaskId = 4, Text = "Choose a dog and a cat"},
            };
            var bodies = new TextBody[]
            {
                new (){Id = 1, Text = "2 + 2 =", TaskId = 1,},
                new (){Id = 3, Text = "Match similar", TaskId = 3,},
                new (){Id = 5, Text = "3 + 1 =", TaskId = 5,},
            };


            var tasks = new ChooseFromListTask[]
            {
                new (){Id = 1, TestId = 1, Description = "ChooseFromListTask", Point = 10, BodyId = 1,},
                new (){Id = 2, TestId = 1, Description = "ChooseFromListTask", Point = 10, BodyId = 2,},
            };
            var matchTasks = new MatchTask[]
            {
                new (){Id = 3, TestId = 1, Description = "MatchTask", Point = 10, BodyId = 3,},
            };
            var multipleSelectTasks = new MultipleSelectTask[]
            {
                new (){Id = 4, TestId = 1, Description = "MultipleChoiceTask", Point = 10, BodyId = 4,},
            };
            var enterTextTasks = new EnterTextTask[]
            {
                new (){Id = 5, TestId = 1, Description = "EnterTextTask", Point = 10, BodyId = 5,},
            };



            var tests = new Test[]
            {
                new (){Id = 1, Title = "Test1", Description = "Test1", InfoForTestTaker = "Test1", Author = "Admin", CreatedAt = new(2010, 10, 10), PassingPercent = 50, IsArchived = false,}
            };
            var asignedTests = new TestAssigned[]
            {
                new (){Id = 1, TestId = 1, CreatedAt = new(2023,12,30), StartAt = new(2023,12,30), EndAt = new(2024,01,02), TimeToTake = new TimeSpan(1,0,0), IsArchived = false,}
            };
            var asignedUsers = new TestAssignedUser[]
            {
                new(){Id = 1, UserId = 2, TestAssignedId = 1, IsActive = true, AppointmentDate = new(2023,12,30)},
                new(){Id = 2, UserId = 3, TestAssignedId = 1, IsActive = false, AppointmentDate = new(2023,12,30)},
                new(){Id = 3, UserId = 4, TestAssignedId = 1, IsActive = false, AppointmentDate = new(2023,12,30)},
                new(){Id = 4, UserId = 1, TestAssignedId = 1, IsActive = true, AppointmentDate = new(2023,12,30)}
            };


            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<UserGroup>().HasData(userGroups);

            modelBuilder.Entity<ChooseFromListTask>().HasData(tasks);
            modelBuilder.Entity<MatchTask>().HasData(matchTasks);
            modelBuilder.Entity<MultipleSelectTask>().HasData(multipleSelectTasks);
            modelBuilder.Entity<EnterTextTask>().HasData(enterTextTasks);

            modelBuilder.Entity<TextBody>().HasData(bodies);
            modelBuilder.Entity<ImageBody>().HasData(imageBodies);

            modelBuilder.Entity<TextAnswer>().HasData(textAnswer);
            modelBuilder.Entity<ImageAnswer>().HasData(imageAnswer);
            modelBuilder.Entity<EnterTextAnswer>().HasData(enterTextAnswer);

            modelBuilder.Entity<Test>().HasData(tests);
            modelBuilder.Entity<TestAssigned>().HasData(asignedTests);
            modelBuilder.Entity<TestAssignedUser>().HasData(asignedUsers);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");
        }
    }
}