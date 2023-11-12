using Microsoft.EntityFrameworkCore;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Bodies;
using TestLib.Classes.Tasks;
using TestLib.Classes.Test;
using TestLib.Interfaces;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB
{
    public class TestDBContext : DbContext
    {
        public DbSet<Test> Test { get; set; }

        public DbSet<Task> Task { get; set; }
        public DbSet<ChooseFromListTask> ChooseFromListTask { get; set; }

        public DbSet<Body> Body { get; set; }
        public DbSet<TextBody> TextBody { get; set; }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<TextAnswer> TextAnswer { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<UserTestResult> UserTestResult { get; set; }
        public DbSet<UserTaskResult> UserTaskResults { get; set; }
        public DbSet<UserAnswerResult> UserAnswerResults { get; set; }
        
        public DbSet<TestAssigned> TestAssigned { get; set; }
        public DbSet<TestAssignedUser> TestAssignedUser { get; set; }

        //public DbSet<TestResult> TestResult { get; set; }
        //public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        //{
        //}
        public TestDBContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });
            modelBuilder.Entity<Task>().UseTptMappingStrategy();
            modelBuilder.Entity<Answer>().UseTptMappingStrategy();
            modelBuilder.Entity<Body>().UseTptMappingStrategy();
            var users = new User[]
            {
                new User(){Id = 1, FirstName = "Admin", LastName = "Admin", Login = "admin", Password = "admin", Role = UserRole.Admin, CreatedAt = new (2000,02,15), IsArchived = false },
                new User(){Id = 2, FirstName = "User", LastName = "User", Login = "user", Password = "user", Role = UserRole.User, CreatedAt = new (2000,01,12), IsArchived = false },
                new User(){Id = 3, FirstName = "User1", LastName = "User1", Login = "user1", Password = "user1", Role = UserRole.User, CreatedAt = new (2000,03,11), IsArchived = true},
                new User(){Id = 4, FirstName = "User2", LastName = "User2", Login = "user2", Password = "user2", Role = UserRole.User, CreatedAt = new (2000,05,21), IsArchived = false}
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
                new UserGroup(){UserId = 4, GroupId = 2}
            };
            var ansvers = new TextAnswer[]
            {
                new TextAnswer(){Id = 1, Text = "1", IsCorrect = false, TaskId = 1},
                new TextAnswer(){Id = 2, Text = "2", IsCorrect = false, TaskId = 1},
                new TextAnswer(){Id = 3, Text = "3", IsCorrect = false, TaskId = 1},
                new TextAnswer(){Id = 4, Text = "4", IsCorrect = true, TaskId = 1},
            };
            var bodies = new TextBody[]
            {
                new TextBody(){Id = 1, Text = "2+2 =", TaskId = 1},
            };
            var tasks = new ChooseFromListTask[]
            {
                new ChooseFromListTask(){Id = 1, TestId = 1, Description = "ChooseFromListTask", Point = 10, BodyId = 1},
            };
            var tests = new Test[]
            {
                new Test(){Id = 1, Title = "Test1", Description = "Test1", InfoForTestTaker = "Test1", Author = "Admin", CreatedAt = new(2010, 10, 10), PassingPercent = 50, IsArchived = false}
            };
            var asignedTests = new TestAssigned[]
            {
                new TestAssigned(){Id = 1, TestId = 1, CreatedAt = new(2023,12,30), StartAt = new(2023,12,30), EndAt = new(2024,01,02), TimeToTake = new TimeSpan(1,0,0), IsArchived = false}
            };
            var asignedUsers = new TestAssignedUser[]
            {
                new TestAssignedUser(){Id = 1, UserId = 2, TestAssignedId = 1},
                new TestAssignedUser(){Id = 2, UserId = 3, TestAssignedId = 1},
                new TestAssignedUser(){Id = 3, UserId = 4, TestAssignedId = 1}
            };
            var userTestResult = new UserTestResult[]
            {
                new UserTestResult(){Id = 1, TestAssignedUserId = 1, PassageDate = new DateTime(2023,12,31)},
                new UserTestResult(){Id = 2, TestAssignedUserId = 2, PassageDate = new DateTime(2023,12,31)},
                new UserTestResult(){Id = 3, TestAssignedUserId = 3, PassageDate = new DateTime(2023,12,31)},
            };
            var userTaskResult = new UserTaskResult[]
            {
                new UserTaskResult(){Id = 1, UserTestResultId = 1, TaskId = 1},
                new UserTaskResult(){Id = 2, UserTestResultId = 2, TaskId = 1},
                new UserTaskResult(){Id = 3, UserTestResultId = 3, TaskId = 1},
            };
            var userAnswerResult = new UserAnswerResult[]
            {
                new UserAnswerResult(){Id = 1, UserTaskResultId = 1, AnswerId = 1},
                new UserAnswerResult(){Id = 2, UserTaskResultId = 2, AnswerId = 2},
                new UserAnswerResult(){Id = 3, UserTaskResultId = 3, AnswerId = 3},
            };


            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<UserGroup>().HasData(userGroups);
            modelBuilder.Entity<TextAnswer>().HasData(ansvers);
            modelBuilder.Entity<TextBody>().HasData(bodies);
            modelBuilder.Entity<ChooseFromListTask>().HasData(tasks);
            modelBuilder.Entity<Test>().HasData(tests);
            modelBuilder.Entity<TestAssigned>().HasData(asignedTests);
            modelBuilder.Entity<TestAssignedUser>().HasData(asignedUsers);
            modelBuilder.Entity<UserTestResult>().HasData(userTestResult);
            modelBuilder.Entity<UserTaskResult>().HasData(userTaskResult);
            modelBuilder.Entity<UserAnswerResult>().HasData(userAnswerResult);



            modelBuilder.Entity<Test>()
                .HasMany(t => t.Tasks)
                .WithOne(task => task.Test)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .HasMany(task => task.Answers)
                .WithOne(answer => answer.Task)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .HasOne(task => task.Body)
                .WithOne(body => body.Task)
                .HasForeignKey<Body>(Body => Body.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(answer => answer.Task)
                .WithMany(task => task.Answers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Body>()
                .HasOne(body => body.Task)
                .WithOne(task => task.Body)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTestResult>()
                .HasMany(t=>t.UserTaskResults) 
                .WithOne(t => t.UserTestResult) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTaskResult>()
                .HasMany(t=>t.UserAnswerResults)
                .WithOne(t => t.UserTaskResult)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswerResult>()
                .HasOne(t=>t.UserTaskResult)
                .WithMany(t=>t.UserAnswerResults)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTaskResult>()
                .HasOne(x=>x.UserTestResult)
                .WithMany(x => x.UserTaskResults)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Group>().HasMany(x => x.Users).WithMany(x => x.Groups).UsingEntity<UserGroup>();
            modelBuilder.Entity<TestAssigned>().HasMany(x => x.Users).WithMany(x => x.TestAssigneds).UsingEntity<TestAssignedUser>();
                                              
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");
        }
    }
}