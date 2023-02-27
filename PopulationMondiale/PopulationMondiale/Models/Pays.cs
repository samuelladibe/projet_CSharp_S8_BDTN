namespace PopulationMondiale.Models
{
    public class Pays
    {
        public int Id { get; set; }
        public string NomPays { get; set; }
        public ICollection<Population> Population_ { get; set; }

    }
}
