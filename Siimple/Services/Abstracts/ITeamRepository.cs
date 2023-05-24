using Siimple.Models;

namespace Siimple.Services.Abstracts
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();
        Team GetTeamById(int id);
        void Create(Team team);
        void Update(int id);
        void Delete(int id);
    }
}
