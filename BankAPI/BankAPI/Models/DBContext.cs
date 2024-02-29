using Microsoft.EntityFrameworkCore;

namespace BankAPI.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
