using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.Areas.Identity.Data;
using Movie_Tracker.Models;

namespace Movie_Tracker.Data
{
    public class MovieContext : IdentityDbContext<ApplicationUser>
    {
        public MovieContext(DbContextOptions<MovieContext> options) :base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorRole> ActorRoles { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<ActorRole>().ToTable("ActorRole");
            modelBuilder.Entity<Director>().ToTable("Director");
            modelBuilder.Entity<WatchedMovie>().ToTable("WatchedMovie");

            modelBuilder.Entity<WatchedMovie>()
                .HasOne(ub => ub.ApplicationUser)
                .WithMany(au => au.WatchedMovies)
                .HasForeignKey(ub => ub.ApplicationUserId);
            modelBuilder.Entity<WatchedMovie>()
                .HasOne(ub => ub.Movie)
                .WithMany(b => b.WatchedMovies) 
                .HasForeignKey(ub => ub.MovieId);
        }
    }
}
