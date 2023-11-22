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

        public DbSet<Body> Body { get; set; }
        public DbSet<TextBody> TextBody { get; set; }
        public DbSet<ImageBody> ImageBody { get; set; }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<TextAnswer> TextAnswer { get; set; }
        public DbSet<ImageAnswer> ImageAnswer { get; set; }
        public DbSet<MatchAnswer> MatchAnswer { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<UserTestResult> UserTestResult { get; set; }
        public DbSet<UserTaskResult> UserTaskResults { get; set; }
        public DbSet<UserAnswerResult> UserAnswerResults { get; set; }

        public DbSet<TestAssigned> TestAssigned { get; set; }
        public DbSet<TestAssignedUser> TestAssignedUser { get; set; }

        public TestDBContext()
        {

        }
        public TestDBContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
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
                new User(){Id = 1, FirstName = "Admin", LastName = "Admin", Login = "admin", Password = encryptor.Encrypt("admin"), Role = UserRole.Admin, CreatedAt = new (2000,02,15), IsArchived = false },
                new User(){Id = 2, FirstName = "User", LastName = "User", Login = "user", Password = encryptor.Encrypt("user"), Role = UserRole.User, CreatedAt = new (2001,01,12), IsArchived = false },
                new User(){Id = 3, FirstName = "User1", LastName = "User1", Login = "user1", Password = encryptor.Encrypt("user1"), Role = UserRole.User, CreatedAt = new (2002,03,11), IsArchived = true},
                new User(){Id = 4, FirstName = "User2", LastName = "User2", Login = "user2", Password = encryptor.Encrypt("user2"), Role = UserRole.User, CreatedAt = new (2005,05,21), IsArchived = false}
            };
            var groups = new Group[]
            {
                new Group(){Id = 1, Name = "Admins", Description = "Admins group"},
                new Group(){Id = 2, Name = "Users", Description = "Users group"},
            };
            var userGroups = new UserGroup[]
            {
                new UserGroup(){UserId = 1, GroupId = 1},
                new UserGroup(){UserId = 2, GroupId = 2},
                new UserGroup(){UserId = 3, GroupId = 2},
                new UserGroup(){UserId = 4, GroupId = 2,}
            };
            var textAnswer = new TextAnswer[]
            {
                new TextAnswer(){Id = 1, Text = "1", IsCorrect = false, TaskId = 1,},
                new TextAnswer(){Id = 2, Text = "2", IsCorrect = false, TaskId = 1},
                new TextAnswer(){Id = 3, Text = "3", IsCorrect = false, TaskId = 1},
                new TextAnswer(){Id = 4, Text = "4", IsCorrect = true, TaskId = 1,},

                new TextAnswer(){Id = 5, Text = "1", IsCorrect = false, TaskId = 2,},
                new TextAnswer(){Id = 6, Text = "2", IsCorrect = false, TaskId = 2},
                new TextAnswer(){Id = 7, Text = "3", IsCorrect = false, TaskId = 2},
                new TextAnswer(){Id = 8, Text = "4", IsCorrect = true, TaskId = 2,},
            };
            var matchAnswers = new MatchAnswer[]
            {
                new MatchAnswer() { Id = 9, Text = "Image", IsCorrect = true, TaskId = 3, Side = MatchSide.Left, PartnerId = 10 },
                new MatchAnswer() { Id = 10, Text = "Picture", IsCorrect = true, TaskId = 3, Side = MatchSide.Right, PartnerId = 9, },
                new MatchAnswer(){Id = 11, Text = "Apartment", IsCorrect = true, TaskId = 3, Side = MatchSide.Left, PartnerId = 12},
                new MatchAnswer(){Id = 12, Text = "Flat", IsCorrect = true, TaskId = 3, Side = MatchSide.Right, PartnerId = 11},
                //new MatchAnswer(){Id = 13, Text = "Pants", IsCorrect = true, TaskId = 3, Side = MatchSide.Left, PartnerId = 14},
                //new MatchAnswer(){Id = 14, Text = "Trousers", IsCorrect = true, TaskId = 3, Side = MatchSide.Right, PartnerId = 13},

            };
            var imageBodies = new ImageBody[]
            {
                new ImageBody(){Id = 2, ImagePath = "/Messenger-icon.png", ImageLength = 3993, TaskId = 2,},
            };
            var bodies = new TextBody[]
            {
                new TextBody(){Id = 1, Text = "2+2 =", TaskId = 1,},
                new TextBody(){Id = 3, Text = "Match similar", TaskId = 3,},
            };
            var tasks = new ChooseFromListTask[]
            {
                new ChooseFromListTask(){Id = 1, TestId = 1, Description = "ChooseFromListTask", Point = 10, BodyId = 1,},
                new ChooseFromListTask(){Id = 2, TestId = 1, Description = "ChooseFromListTask", Point = 10, BodyId = 2,},
            };
            var matchTasks = new MatchTask[]
            {
                new MatchTask(){Id = 3, TestId = 1, Description = "MatchTask", Point = 10, BodyId = 3,},
            };
            var tests = new Test[]
            {
                new Test(){Id = 1, Title = "Test1", Description = "Test1", InfoForTestTaker = "Test1", Author = "Admin", CreatedAt = new(2010, 10, 10), PassingPercent = 50, IsArchived = false,}
            };
            var asignedTests = new TestAssigned[]
            {
                new TestAssigned(){Id = 1, TestId = 1, CreatedAt = new(2023,12,30), StartAt = new(2023,12,30), EndAt = new(2024,01,02), TimeToTake = new TimeSpan(1,0,0), IsArchived = false,}
            };
            var asignedUsers = new TestAssignedUser[]
            {
                new TestAssignedUser(){Id = 1, UserId = 2, TestAssignedId = 1, IsActive = true, AppointmentDate = new(2023,12,30)},
                new TestAssignedUser(){Id = 2, UserId = 3, TestAssignedId = 1, IsActive = false, AppointmentDate = new(2023,12,30)},
                new TestAssignedUser(){Id = 3, UserId = 4, TestAssignedId = 1, IsActive = false, AppointmentDate = new(2023,12,30)},
                new TestAssignedUser(){Id = 4, UserId = 1, TestAssignedId = 1, IsActive = true, AppointmentDate = new(2023,12,30)}
            };


            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<UserGroup>().HasData(userGroups);
            modelBuilder.Entity<Test>().HasData(tests);
            modelBuilder.Entity<ChooseFromListTask>().HasData(tasks);
            modelBuilder.Entity<MatchTask>().HasData(matchTasks);
            modelBuilder.Entity<MatchAnswer>().HasData(matchAnswers);
            modelBuilder.Entity<TextBody>().HasData(bodies);
            modelBuilder.Entity<ImageBody>().HasData(imageBodies);
            modelBuilder.Entity<TextAnswer>().HasData(textAnswer);
            modelBuilder.Entity<TestAssigned>().HasData(asignedTests);
            modelBuilder.Entity<TestAssignedUser>().HasData(asignedUsers);















            //var userTestResult = new UserTestResult[]
            //{
            //    new UserTestResult(){Id = 2, TestAssignedUserId = 2, PassageDate = new DateTime(2023,12,31)},
            //    new UserTestResult(){Id = 3, TestAssignedUserId = 3, PassageDate = new DateTime(2023,12,31)},
            //};
            //var userTaskResult = new UserTaskResult[]
            //{
            //    new UserTaskResult(){Id = 2, UserTestResultId = 2, TaskId = 1},
            //    new UserTaskResult(){Id = 3, UserTestResultId = 3, TaskId = 1},
            //};
            //var userAnswerResult = new UserAnswerResult[]
            //{
            //    new UserAnswerResult(){Id = 2, UserTaskResultId = 2, AnswerId = 2},
            //    new UserAnswerResult(){Id = 3, UserTaskResultId = 3, AnswerId = 3},
            //};
            //modelBuilder.Entity<UserTestResult>().HasData(userTestResult);
            //modelBuilder.Entity<UserTaskResult>().HasData(userTaskResult);
            //modelBuilder.Entity<UserAnswerResult>().HasData(userAnswerResult);

























        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");
        }
    }
}