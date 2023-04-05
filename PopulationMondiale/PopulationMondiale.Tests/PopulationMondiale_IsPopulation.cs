using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulationMondiale.Models;

namespace PopulationMondiale.Tests
{
    public class PopulationMondiale_IsPopulation
    {
        [Fact]
        public void IsPopulation_ReturnTrue()
        {
            var population = new Population
            {
                Annee = 2023,
                Valeur = 250000,
                PaysId = 0
            };
            bool result1 = population.Annee.Equals(2023);
            bool result2 = population.Valeur.Equals(250000);

            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
