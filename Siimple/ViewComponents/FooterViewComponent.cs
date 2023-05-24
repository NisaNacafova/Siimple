using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;

namespace Siimple.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly SiimpleDbContext _db;
        public FooterViewComponent(SiimpleDbContext simple)
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
