using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;

namespace CreateNotbookSystem.Service.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
            public UserRepository(MyNotbookContext dbContext) : base(dbContext)
            {
            }
    }
}
