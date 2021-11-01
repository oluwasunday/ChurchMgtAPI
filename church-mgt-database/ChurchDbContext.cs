using church_mgt_models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace church_mgt_database
{
    public class ChurchDbContext : IdentityDbContext<AppUser>
    {
        public ChurchDbContext(DbContextOptions<ChurchDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PrayerRequest> PrayerRequests { get; set; }
        public DbSet<Support> Supports { get; set; }
    }
}
