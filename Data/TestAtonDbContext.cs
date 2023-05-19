using Data.EntityConfigurations;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TestAtonDbContext : DbContext
    {
        public TestAtonDbContext(DbContextOptions<TestAtonDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
