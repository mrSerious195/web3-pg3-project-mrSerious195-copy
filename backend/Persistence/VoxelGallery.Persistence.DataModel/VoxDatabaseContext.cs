using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VoxelGallery.Persistence.DataModel.Entities;

namespace VoxelGallery.Persistence.DataModel;
// test
public class VoxDatabaseContext(IOptions<DbOptions> options):DbContext
{
      public DbSet<Comment> Comments { get; set; } 
    public DbSet<User> Users { get; set; } 
    public DbSet<Scene> Scenes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         optionsBuilder.UseAzureSql(  
        options.Value.connectionString,
              builder=>  builder.MigrationsHistoryTable(
                "__EFMigrationsHistory","VoxData") 
        );
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("VoxData");

        modelBuilder.Entity<User>()
        .HasMany(u=>u.Favorites)
        .WithMany(s=>s.Favorites)
        .UsingEntity("Favorites");

         modelBuilder.Entity<User>()
        .HasMany(u=>u.Likes)
        .WithMany(s=>s.Likes)
        .UsingEntity("Likes");

         modelBuilder.Entity<User>()
        .HasMany(u=>u.MadeScenes)
        .WithOne(s=>s.Uploader)
        .HasForeignKey(s => s.UploaderId);

        modelBuilder.Entity<Comment>()
    .HasOne(c => c.Scene)
    .WithMany(s => s.Comments)
    .HasForeignKey(c => c.SceneID);

modelBuilder.Entity<Comment>()
    .HasOne(c => c.User)
    .WithMany(u => u.Comments)
    .HasForeignKey(c => c.UserId);

    }
}
