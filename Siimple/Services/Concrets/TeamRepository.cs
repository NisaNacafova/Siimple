using Siimple.DataContext;
using Siimple.Models;
using Siimple.Services.Abstracts;

namespace Siimple.Services.Concrets
{
    public class TeamRepository:ITeamRepository
    {
        private readonly SiimpleDbContext _db;
        public TeamRepository(SiimpleDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Team> GetTeams()
        {
            return _db.Team.ToList();
        }
        public Team GetTeamById(int id)
        {
            return _db.Team.Find(id);
        }
        public void Create(Team team)
        {
            _db.Team.Add(team);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db.Team.Remove(GetTeamById(id));
            _db.SaveChanges();
        }
        public void Update(int id)
        {
            _db.Update(GetTeamById(id));
            _db.SaveChanges();
        }
    }
}
