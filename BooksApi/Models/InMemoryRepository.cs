using Microsoft.EntityFrameworkCore;

namespace BooksApi.Models
{
    
    public class InMemoryRepository : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public InMemoryRepository(DbContextOptions<InMemoryRepository> options)
            : base(options)
        {
        }
    }
}
