using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CreateNotbookSystem.Service.Repository
{
    public class BacklogRepository : Repository<Backlog>, IRepository<Backlog>
    {
        public BacklogRepository(MyNotbookContext dbContext) : base(dbContext)
        {
        }
    }
}
