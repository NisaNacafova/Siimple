using Microsoft.EntityFrameworkCore;
using Siimple.DataContext;
using Siimple.Models;
using Siimple.Services.Abstracts;

namespace Siimple.Services.Concrets
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SiimpleDbContext _db;
        public Repository(SiimpleDbContext db)
        {
            _db = db;
        }
        public void Create(T t)
        {
            _db.Set<T>().Add(t);
        }

        public void Delete(int id)
        {
            _db.Set<T>().Remove(GetTeamById(id));

        }

        public T GetTeamById(int id)
        {
           return _db.Set<T>().FirstOrDefault(x=>x.Id==id);
        }

        public IEnumerable<T> GetTeams(string? includes)
        {
            IQueryable<T> query = _db.Set<T>().AsQueryable();
            if (includes != null)
            {
                 query = query.Include(includes);
            }
           return  query.ToList();
        }
        public void Update(int id, string includes = null, T model = null)
        {
            var oldmodel= _db.Set<T>().Include(includes).FirstOrDefault(x => x.Id == id);
            oldmodel = model;
            _db.Set<T>().Update(oldmodel);
        }
    }
}
