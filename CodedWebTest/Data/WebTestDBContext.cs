using CodedWebTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodedWebTest.Data
{
    public class WebTestDBContext : DbContext
    {
        public WebTestDBContext()
        {
        }

        public WebTestDBContext(DbContextOptions<WebTestDBContext> options) : base(options) { }

        public virtual DbSet<EmailAddress> EmailAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("WebTestDatabase");
            }
        }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailAddress>(x => x.HasKey(p => p.EmailAddressUid));
          
        }
    }
}