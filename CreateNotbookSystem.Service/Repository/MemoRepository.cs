using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CreateNotbookSystem.Service.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(MyNotbookContext dbContext) : base(dbContext)
        {
        }
    }
}
