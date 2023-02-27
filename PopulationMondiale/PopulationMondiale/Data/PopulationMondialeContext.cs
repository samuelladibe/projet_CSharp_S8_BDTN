using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopulationMondiale.Models;

namespace PopulationMondiale.Data
{
    public class PopulationMondialeContext : DbContext
    {
        public PopulationMondialeContext (DbContextOptions<PopulationMondialeContext> options)
            : base(options)
        {
        }

        public DbSet<PopulationMondiale.Models.Pays> Pays { get; set; } = default!;

        public DbSet<PopulationMondiale.Models.Continent> Continent { get; set; }

        public DbSet<PopulationMondiale.Models.Population> Population { get; set; }
    }
}
