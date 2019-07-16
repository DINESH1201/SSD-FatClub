﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FatClub.Models
{
    public class FatClubContext : IdentityDbContext<ApplicationUser, ApplicationRole,string>
    {
        public FatClubContext (DbContextOptions<FatClubContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        public DbSet<FatClub.Models.Food> Food { get; set; }
        public DbSet<FatClub.Models.AuditLog> AuditLogs { get; set; }
    }
}
