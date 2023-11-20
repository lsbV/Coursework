using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALTestsDB;

namespace Server.Infrastructure
{
    public class SampleMyDbContextFactory : IDesignTimeDbContextFactory<TestDBContext>
    {
        public TestDBContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            object value = builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<TestDBContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            return new TestDBContext(options);
        }
    }
}
