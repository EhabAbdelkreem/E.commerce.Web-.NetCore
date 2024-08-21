using E.Comerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E.Comerce.DataAccess_Layer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

    }
}
