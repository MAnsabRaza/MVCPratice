using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC_Pratice.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(): base("DefaultConnection") { }
        public DbSet<Student> Student {  get; set; }
        public DbSet<User> User { get; set; }
    }
}