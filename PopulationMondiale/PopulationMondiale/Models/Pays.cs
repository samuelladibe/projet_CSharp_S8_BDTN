using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopulationMondiale.Models
{
    public class Pays
    {
        public int Id { get; set; }

        [Required] public string NomPays { get; set; }

        [ForeignKey("Continent")]
        [Required]
        public int ContinentId { get; set; }
        public ICollection<Population> Population_ { get; set; }

    }
}
