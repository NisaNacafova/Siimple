using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;
using Siimple.Services.Abstracts;

namespace Siimple.Services.Concrets
{
    public class TeamRepository:Repository<Team>,ITeamRepository
    {
        private readonly SiimpleDbContext _db;
        public TeamRepository(SiimpleDbContext db):base(db)
        {
            _db = db;
        }
    }
}
