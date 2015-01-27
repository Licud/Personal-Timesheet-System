using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("TimesheetDatabaseConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Project> Project { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<TimeLog> TimeLog { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}