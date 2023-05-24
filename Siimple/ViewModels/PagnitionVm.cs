using Siimple.Models;

namespace Siimple.ViewModels
{
    public class PagnitionVm
    {
        public List<Team> Teams { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
