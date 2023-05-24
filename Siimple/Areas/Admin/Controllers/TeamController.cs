using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Extensions;
using Siimple.Models;
using Siimple.ViewModels;
using Siimple.ViewModels.TeamVm;
using System.Data;

namespace Siimple.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly SiimpleDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public TeamController(SiimpleDbContext simple,IWebHostEnvironment web)
        {
            _db = simple;
            _environment = web;
        }
        public IActionResult Index(int page = 1, int take = 5)
        {
            List<Team> teams = _db.Team.Skip((page-1)*take).Take(take).Include(p=>p.Title).ToList();
            int Totalteamcount = _db.Team.Count();
            PagnitionVm teamsvm = new PagnitionVm()
            {
                Teams = teams,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling((double)Totalteamcount / take)
            };
            //List<GetTeamVm> teamsvm = new List<GetTeamVm>();
            //foreach(Team team in teams)
            //{
            //    teamsvm.Add(new GetTeamVm()
            //    {
            //        Id = team.Id,
            //        Title = team.Title,
            //        TitleId = team.TitleId,
            //        Iconname = team.Iconname,
            //        Imagename = team.Imagename,
            //        Bio = team.Bio
            //    });
            //}
            return View(teamsvm);
        }
        public IActionResult Create()
        {
            List<Title> title=_db.Title.ToList();
            ViewData["Title"] = title;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamVm teamvm)
        {
            List<Title> title = await _db.Title.ToListAsync();
            if(!ModelState.IsValid)
            {
                ViewData["Title"] = title;
                return View(teamvm);
            }
            if(!teamvm.Image.CheckType("image/") && !teamvm.Image.CheckSize(2048))
            {
                ModelState.AddModelError("Image", "Size or Type incorrect");
                return View(teamvm);
            }
            string FileName = await teamvm.Image.Upload(_environment.WebRootPath, "assets", "img");
            //string FileName = Guid.NewGuid().ToString() + teamvm.Image.FileName;
            //string path = Path.Combine(_environment.WebRootPath, "assets", "img", FileName);
            //using(FileStream file=new FileStream(path, FileMode.CreateNew))
            //{
            //    await teamvm.Image.CopyToAsync(file);
            //}
            Team team = new Team()
            {
                Bio = teamvm.Bio,
                Iconname = teamvm.Iconname,
                TitleId = teamvm.TitleId,
                Imagename = FileName
            };
            _db.Team.Add(team); 
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Team? team= _db.Team.FirstOrDefault(i=>i.Id == id);
            if(team==null) return NotFound();
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", team.Imagename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _db.Team.Remove(team);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Team? team = _db.Team.Include(i=>i.Title).FirstOrDefault(i=>i.Id == id);
            if (team == null) return NotFound();
            EditTeamVm teamvm = new EditTeamVm()
            {
                Id = id,
                Bio = team.Bio,
                Image = team.Image,
                TitleId = team.TitleId,
                Iconname = team.Iconname,
            };
            List<Title> title = _db.Title.ToList();
            ViewData["Title"] = title;
            return View(teamvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EditTeamVm teamvm)
        {
            Team? team = _db.Team.Include(i => i.Title).FirstOrDefault(i => i.Id == id);
            if (team == null) return NotFound();
            List<Title> title = _db.Title.ToList();
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = title;
                return View(teamvm);
            }
            if(teamvm.Image != null)
            {
                if (!teamvm.Image.CheckType("image/") && !teamvm.Image.CheckSize(2048))
                {
                    ModelState.AddModelError("Image", "Size or Type incorrect");
                    return View(teamvm);
                }
                string path = Path.Combine(_environment.WebRootPath, "assets", "img", team.Imagename);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string FileName = await teamvm.Image.Upload(_environment.WebRootPath, "assets", "img");
                //string FileName = Guid.NewGuid().ToString() + teamvm.Image.FileName;
                //string pathEdit = Path.Combine(_environment.WebRootPath, "assets", "img", FileName);
                //using (FileStream file = new FileStream(pathEdit, FileMode.Create))
                //{
                //    await teamvm.Image.CopyToAsync(file);
                //}
                team.Imagename = FileName;
            }
            team.Bio = teamvm.Bio;
            team.TitleId=teamvm.TitleId;
            team.Iconname = teamvm.Iconname;
            _db.Team.Update(team);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            Team? team = _db.Team.FirstOrDefault(i => i.Id == id);
            if (team == null) return NotFound();
            DetailTeamVm vm = new DetailTeamVm()
            {
                Id = id,
                Bio = team.Bio,
                IconName = team.Iconname,
                ImageName = team.Imagename,
                Title = team.Title
            };
            return View(vm);
        }
    }
}
