using System.Threading.Tasks;
using VictoryLinkTask.Data;
using VictoryLinkTask.Repositories.Interfaces;

namespace VictoryLinkTask.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VictoryLinkDBEntities AppDbContext;

        public UnitOfWork(VictoryLinkDBEntities appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public IRepository<Request> RequestRepository => new Repository<Request>(AppDbContext);

        public async Task<int> Commit()
        {
            return await AppDbContext.SaveChangesAsync();
        }
    }
}