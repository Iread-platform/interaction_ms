using iread_interaction_ms.DataAccess.Data.Entity;
using Microsoft.EntityFrameworkCore;

using System;

namespace iread_interaction_ms.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Comment>()
            .HasOne(c => c.Interaction)
            .WithMany(i => i.Comments)
            .OnDelete(DeleteBehavior.Cascade);
        }

        //entities
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Audio> Audios { get; set; }
         public DbSet<Comment> Comments { get; set; }
         public DbSet<Drawing> Drawings { get; set; }

    }
}
