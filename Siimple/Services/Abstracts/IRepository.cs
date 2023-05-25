using Siimple.Models;

namespace Siimple.Services.Abstracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetTeams(string includes=null);
        T GetTeamById(int id);
        void Create(T t);
        void Update(int id, string includes = null, T model=null);
        void Delete(int id);
    }
}
