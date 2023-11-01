using Microsoft.EntityFrameworkCore;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Bodies;
using TestLib.Classes.Tasks;
using TestLib.Interfaces;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB
{
    public class TestDBContext : DbContext
    {
        public DbSet<Test> Test { get; set; }

        public DbSet<Task> Task { get; set; }
        public DbSet<ChooseFromListTask> ChooseFromListTask { get; set; }

        public DbSet<TaskBody> TaskBody { get; set; }
        public DbSet<TextTaskBody> TextTaskBody { get; set; }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<TextAnswer> TextAnswer { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }

        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserTest> UserTest { get; set; }
        public DbSet<UserTask> UserTask { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }

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
            modelBuilder.Entity<TaskBody>().UseTptMappingStrategy();
            var users = new User[]
            {
                new User(){Id = 1, FirstName = "Admin", LastName = "Admin", Login = "admin", Password = "admin", Role = UserRole.Admin, CreatedAt = DateTime.Now, IsArchived = false },
                new User(){Id = 2, FirstName = "User", LastName = "User", Login = "user", Password = "user", Role = UserRole.User, CreatedAt = DateTime.Now, IsArchived = false },
            };
            modelBuilder.Entity<User>().HasData(users);
            var groups = new Group[]
            {
                new Group(){Id = 1, Name = "Admins", Description = "Admins group"},
                new Group(){Id = 2, Name = "Users", Description = "Users group"},
            };
            modelBuilder.Entity<Group>().HasData(groups);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");
        }
    }
}