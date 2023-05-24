using Siimple.Models;

namespace Siimple.ViewModels.TeamVm
{
    public class GetTeamVm
    {
        public int Id { get; set; }
        public int TitleId { get; set; }
        public string? Iconname { get; set; }
        public string? Bio { get;set; }
        public string Imagename { get; set; } = null!;
        public Title Title { get; set; }
    }
}
