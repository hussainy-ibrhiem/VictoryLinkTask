using System.Threading.Tasks;
using VictoryLinkTask.Data;

namespace VictoryLinkTask.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        IRepository<Request> RequestRepository { get; }
    }
}
