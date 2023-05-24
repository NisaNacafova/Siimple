using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;
using Siimple.ViewModels.SettingVm;
using System.Data;

namespace Siimple.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly SiimpleDbContext _db;
        public SettingController(SiimpleDbContext simple)
        {
            _db = simple;
        }
        public IActionResult Index()
        {
            List<Setting> settings = _db.Setting.ToList();
            List<GetSettingVm> settingsvm = new List<GetSettingVm>();
            foreach (Setting setting in settings)
            {
                settingsvm.Add(new GetSettingVm()
                {
                    Id = setting.Id,
                    Value = setting.Value,
                    Key = setting.Key,
                });
            }
            return View(settingsvm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSettingVm settingvm)
        {

            if (!ModelState.IsValid)
            {

                return View(settingvm);
            }

            Setting setting = new Setting()
            {
                Key = settingvm.Key,
                Value = settingvm.Value,
            };
            _db.Setting.Add(setting);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Setting setting = _db.Setting.FirstOrDefault(x => x.Id == id);
            if (setting == null) return NotFound();
            _db.Setting.Remove(setting);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Setting setting = _db.Setting.FirstOrDefault(x => x.Id == id);
            if (setting == null) return NotFound();
            EditSettingVm settingVm = new EditSettingVm()
            {
                Id = id,
                Value = setting.Value,
            };
            return View(settingVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditSettingVm settingVm)
        {
            Setting setting = _db.Setting.FirstOrDefault(x => x.Id == id);
            if (setting == null) return NotFound();
            setting.Value = settingVm.Value;
            _db.Setting.Update(setting);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
