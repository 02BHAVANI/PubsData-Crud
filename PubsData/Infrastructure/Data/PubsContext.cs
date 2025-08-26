using Microsoft.EntityFrameworkCore;
using PubsData.Domain.Entities;

namespace PubsData.Infrastructure.Data
{
    public class PubsContext : DbContext
    {
        public PubsContext(DbContextOptions<PubsContext> options) : base(options) { }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Title> Titles => Set<Title>();
        public DbSet<Sales> Sales => Set<Sales>();
        public DbSet<Store> Stores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.AuId);
            modelBuilder.Entity<Publisher>().HasKey(p => p.PubId);
            modelBuilder.Entity<Title>().HasKey(t => t.TitleId);
            modelBuilder.Entity<Sales>().HasKey(s => new { s.StorId, s.OrdNum, s.TitleId });
        }

    }

}