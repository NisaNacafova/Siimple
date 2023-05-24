using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;

namespace Siimple.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly SiimpleDbContext _db;
        public HeaderViewComponent(SiimpleDbContext simple)
        {
            _db = simple;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, Setting> settings = await _db.Setting.ToDictionaryAsync(c => c.Key);
            return View(settings);
        }
    }
}
