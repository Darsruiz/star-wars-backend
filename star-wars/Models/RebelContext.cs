using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.Models
{
    public class RebelContext : DbContext
    {
        public RebelContext(DbContextOptions<RebelContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Rebel> Rebels { get; }
    }
}
