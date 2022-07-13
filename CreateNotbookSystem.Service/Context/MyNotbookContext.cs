using Microsoft.EntityFrameworkCore;

namespace CreateNotbookSystem.Service.Context
{
    public class MyNotbookContext : DbContext
    {
        public DbSet<Backlog> Backlog { get; set; }
        public DbSet<Memo> Memo { get; set; }
        public DbSet<User> User { get; set; }

        public MyNotbookContext(DbContextOptions<MyNotbookContext> options): base(options)
        {

        }
    }
}
