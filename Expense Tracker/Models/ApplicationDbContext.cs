using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor that takes DbContextOptions and passes them to the base class constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        // DbSet representing the Transactions table in the database
        public DbSet<Transaction> Transactions { get; set; }

        // DbSet representing the Categories table in the database
        public DbSet<Category> Categories { get; set; }
    }
}
