using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopulationMondiale.Models
{
    public class Population
    {
        public int Id { get; set; }
        [Required]  public int Annee { get; set; }
        [Required]  public double Valeur { get; set; }

        [ForeignKey("Pays")][Required]
        public int PaysId { get; set; }  
    }
}
