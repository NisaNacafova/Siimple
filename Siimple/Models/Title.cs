namespace Siimple.Models
{
    public class Title:BaseEntity
    {
       
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
}
