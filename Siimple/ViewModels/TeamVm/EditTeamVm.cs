namespace Siimple.ViewModels.TeamVm
{
    public class EditTeamVm
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string Iconname { get; set; }
        public IFormFile Image { get; set; }
        public int TitleId { get; set; }
    }
}
