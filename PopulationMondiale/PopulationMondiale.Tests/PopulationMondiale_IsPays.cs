using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulationMondiale.Models;

namespace PopulationMondiale.Tests
{
    public class PopulationMondiale_IsPays
    {
        [Fact]
        public void IsPays_ReturnTrue()
        {
            var pays = new Pays
            {
                NomPays = "UK",
                ContinentId = 0,
                Population_ = new List<Population>()
            };
            bool result = pays.NomPays.Equals("UK");

            Assert.True(result);
        }
    }
}
