using Microsoft.Build.Framework;

namespace PopulationMondiale.Models
{
    public class Continent
    {
        public int Id { get; set; }

        [Required] public string NomContinent { get; set; }
        public ICollection<Pays> Pays_ { get; set; }

    }
}
