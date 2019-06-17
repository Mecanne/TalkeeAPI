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
                .HasDefaultValue("https://picsum.photos/400");
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Follows> Follows { get; set; }

    }
}
