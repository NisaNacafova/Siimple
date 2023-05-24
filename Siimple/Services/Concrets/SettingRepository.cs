using Siimple.DataContext;
using Siimple.Models;
using Siimple.Services.Abstracts;

namespace Siimple.Services.Concrets
{
    public class SettingRepository:IRepository<Setting>
    {
        private readonly SiimpleDbContext _db;
        public SettingRepository(SiimpleDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Setting> GetTeams()
        {
            return _db.Setting.ToList();
        }
        public Setting GetTeamById(int id)
        {
            return _db.Setting.Find(id);
        }
        public void Create(Setting setting)
        {
            _db.Setting.Add(setting);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db.Setting.Remove(GetTeamById(id));
            _db.SaveChanges();
        }
        public void Update(int id)
        {
            _db.Update(GetTeamById(id));
            _db.SaveChanges();
        }
    }
}
