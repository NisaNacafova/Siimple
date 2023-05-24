using Siimple.Models;

namespace Siimple.ViewModels.TeamVm
{
    public class DetailTeamVm
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string IconName { get; set; }
        public string ImageName { get; set; }
        public Title Title { get; set; }
    }
}
