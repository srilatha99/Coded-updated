using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodedWebTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodedWebTest.Data
{
    public class WebTestDBContext : DbContext
    {
        public WebTestDBContext(DbContextOptions<WebTestDBContext> options) : base(options) { }

        public virtual DbSet<EmailAddress> EmailAddress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailAddress>(x => x.HasKey(p => p.EmailAddressUid));
        }
    }
}