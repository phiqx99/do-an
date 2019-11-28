using BEQLDT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BEQLDT.Data
{
    public class FDbContext : DbContext
    {

        public FDbContext(DbContextOptions<FDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Council> Councils { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<Filed> Files { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<TopicCouncil> TopicCouncils { get; set; }
     }
    }
