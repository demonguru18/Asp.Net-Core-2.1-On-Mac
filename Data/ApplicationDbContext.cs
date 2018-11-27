using System;
using AspCoreCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
