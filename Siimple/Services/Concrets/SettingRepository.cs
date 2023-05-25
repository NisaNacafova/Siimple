using Siimple.DataContext;
using Siimple.Models;
using Siimple.Services.Abstracts;

namespace Siimple.Services.Concrets
{
    public class SettingRepository:Repository<Setting>, ISettingRepository
    {
        private readonly SiimpleDbContext _db;
        public SettingRepository(SiimpleDbContext db):base(db)
        {
            _db = db;
        }
       
    }
}
