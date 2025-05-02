namespace KezdoCsapat.Models
{
    public class Formation
    {
        public string FormationName { get; set; } = string.Empty;
        public List<Player> StartingEleven { get; set; } = new();
    }
}