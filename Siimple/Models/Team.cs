using System.ComponentModel.DataAnnotations.Schema;

namespace Siimple.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Imagename { get; set; }
        public string Iconname { get; set; }
        public string Bio { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
