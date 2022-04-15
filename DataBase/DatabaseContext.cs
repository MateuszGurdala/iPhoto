using System.IO;
using iPhoto.UtilityClasses;
using Microsoft.EntityFrameworkCore;

namespace iPhoto.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ImageEntity> ImageEntities { get; set; }
        public DbSet<PhotoEntity> PhotoEntities { get; set; }
        public DbSet<AlbumEntity> AlbumEntities { get; set; }
        public DbSet<PlaceEntity> PlaceEntities { get; set; }

        public string DbPath { get; }

        public DatabaseContext()
        {
            string path = DataHandler.GetDatabaseDirectory();
            DbPath = Path.Join(path, "Database.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //AlbumEntity
            modelBuilder.Entity<AlbumEntity>()
                .Property(b => b.PhotoCount)
                .HasDefaultValue(0);
            modelBuilder.Entity<AlbumEntity>()
                .Property(b => b.Tags)
                .HasDefaultValue("#none");
            ////PhotoEntity
            modelBuilder.Entity<PhotoEntity>()
                .Property(b => b.AlbumEntityId)
                .HasDefaultValue(1);
            modelBuilder.Entity<PhotoEntity>()
                .Property(b => b.PlaceEntityId)
                .HasDefaultValue(1);
            modelBuilder.Entity<PhotoEntity>()
                .Property(b => b.Tags)
                .HasDefaultValue("#none");
        }
    }
}