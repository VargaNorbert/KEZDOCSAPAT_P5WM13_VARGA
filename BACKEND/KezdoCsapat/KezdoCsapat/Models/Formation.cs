namespace KezdoCsapat.Models
{
    public class Formation
    {
        public string FormationName { get; set; } = string.Empty;
        public List<Player> GK { get; set; } = new();
        public List<Player> DF { get; set; } = new();
        public List<Player> MF { get; set; } = new();
        public List<Player> FW { get; set; } = new();
        public List<Player> SUB { get; set; } = new();
        public double goodness { get; set; }
    }
}