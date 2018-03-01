using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApp.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<AspNetUser> Users { get; set; }
        public DbSet<AspNetRole> Roles { get; set; }
        public DbSet<AspNetUserRole> UserRoles { get; set; }
        public DbSet<AppLoginLog> AppLoginLogs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AspNetLoginLog> ManageLoginLogs { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<InterfaceOperation> InterfaceOperations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
