namespace PopulationMondiale.Models
{
    public class Population
    {
        public int Id { get; set; }
        public Pays Pays_ { get; set; }
        public int Annee { get; set; }
        public double Valeur { get; set; }
    }
}
