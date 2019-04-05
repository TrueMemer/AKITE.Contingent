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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string con = "Server=127.0.0.1;Database=contingent;Uid=root;Pwd=root;";
            optionsBuilder.UseMySQL(con);
        }
    }
}
