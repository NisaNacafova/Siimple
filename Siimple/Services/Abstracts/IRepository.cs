using Siimple.Models;

namespace Siimple.Services.Abstracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetTeams(string includes);
        T GetTeamById(int id);
        void Create(T t);
        void Update(int id,string includes);
        void Delete(int id);
    }
}
