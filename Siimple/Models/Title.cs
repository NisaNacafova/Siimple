namespace Siimple.Models
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
}
