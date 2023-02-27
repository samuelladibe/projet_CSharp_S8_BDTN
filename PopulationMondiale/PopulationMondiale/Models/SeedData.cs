using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PopulationMondiale.Data;
using System;
using System.Linq;

namespace PopulationMondiale.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PopulationMondialeContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<PopulationMondialeContext>>()))
            {
                context.Database.EnsureCreated();
                // S’il y a déjà des pays dans la base
                if (context.Pays.Any())
                {
                    return; // On ne fait rien
                }
                // Sinon on en ajoute un
                context.Pays.AddRange(
                new Pays
                {
                    NomPays = "France",
                    Population_ = new List<Population>()
                }
                );

                // S’il y a déjà des continents dans la base
                if (context.Continent.Any())
                {
                    return; // On ne fait rien
                }
                // Sinon on en ajoute un
                context.Continent.AddRange(
                new Continent
                {
                    NomContinent = "Europe",
                    Pays_ = new List<Pays>()
                }
                );

                // S'il y a déjà des populations dans la base
                if (context.Population.Any())
                {
                    return; // On ne fait rien
                }
                // Sinon on en ajoute un
                context.Population.AddRange(
                new Population
                {
                    Pays_ = new Pays(),
                    Annee = 2023,
                    Valeur = 12457637825
                }
                );

                context.SaveChanges();

            }
        }
    }
}
