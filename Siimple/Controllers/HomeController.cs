using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;
using Siimple.ViewModels;
using System.Diagnostics;

namespace Siimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiimpleDbContext _db;
        public HomeController(SiimpleDbContext simple)
        {
            _db = simple;
        }

        public IActionResult Index()
        {
            List<Team> teams = _db.Team.OrderBy(n=>n.Bio).Take(5).Include(i=>i.Title).ToList();
            HomeVm vm = new HomeVm()
            {
                Teams = teams
            };
            return View(vm);
        }


    }
}