using LighthouseTunes.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LighthouseTunes.Data
{
    public class LighthouseTunesDbContext : DbContext
    {
        private const string _connectionString = "server=localhost; integrated security=True; initial catalog=LighthouseTunesDB; TrustServerCertificate=True";



        public LighthouseTunesDbContext()
        {

        }

        public DbSet<Song> Songs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Favourite> Favourites { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the User entity's primary key
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            // Configure the Song entity's primary key
            modelBuilder.Entity<Song>()
                .HasKey(s => s.SongId);

            // Configure the Favourite entity's primary key
            modelBuilder.Entity<Favourite>()
                .HasKey(f => f.Id);


            // A Favourite must have one User as AddedBy
            // Conversely, a User can have multiple Favourites

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.AddedBy)
                .WithMany(u => u.FavouritesList)
                .HasForeignKey(f => f.AddedById)
                .OnDelete(DeleteBehavior.Restrict);

            // A Favourite must have one Song as SelectedSong
            // Conversely, a Song can appear multiple times as a Favourite
            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.SelectedSong)
                .WithMany(s => s.FavouritesList)
                .HasForeignKey(f => f.SelectedSongId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}




