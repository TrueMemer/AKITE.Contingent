using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKITE.Contingent.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentsContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
