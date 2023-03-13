using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulationMondiale.Models;

namespace PopulationMondiale.Tests
{
     public class PopulationMondiale_IsContinent
    {
        [Fact]
        public void IsContinent_ReturnTrue()
        {
            var continent = new Continent
            {
                NomContinent = "Europe",
                Pays_ = new List<Pays>()
            };
            bool result = continent.NomContinent.Equals("Europe");
            
            Assert.True(result);
        }
    }
}
