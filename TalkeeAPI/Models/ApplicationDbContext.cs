using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkeeAPI.Models;

namespace TalkeeAPI.Models
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .Property(u => u.urlImagen)
                .HasDefaultValue("'picsum.photos/400'");

            modelBuilder.Entity<Followers>()
            .HasKey(f => new { f.UserID, f.FollowerID });

            modelBuilder.Entity<Follows>()
            .HasKey(f => new { f.UserID, f.FollowID });
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Follows> Follows { get; set; }

    }
}
