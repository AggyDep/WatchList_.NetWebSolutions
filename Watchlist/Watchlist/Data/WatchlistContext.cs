using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class WatchlistContext : IdentityDbContext
    <
        User,
        IdentityRole,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
    >
    {
        public WatchlistContext(DbContextOptions<WatchlistContext> options) :
            base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<API.Models.WatchList> WatchLists { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder == null) { throw new ArgumentNullException(nameof(modelBuilder)); }

            //User - Unique Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName })
                .IsUnique(true)
                .HasName("UQ_User_Username");

            //Movie - Unique Indexes
            modelBuilder.Entity<Movie>()
                .HasIndex(m => new { m.Name })
                .IsUnique(true)
                .HasName("UQ_Movie_Name");

            //Genre - Unique Indexes
            modelBuilder.Entity<Genre>()
                .HasIndex(g => new { g.GenreName })
                .IsUnique(true)
                .HasName("UQ_Genre_Name");

            //Movie - Unique Indexes
            modelBuilder.Entity<Actor>()
                .HasIndex(a => new { a.FullName })
                .IsUnique(true)
                .HasName("UQ_Actor_Name");

            //Watchlist
            modelBuilder.Entity<Models.WatchList>()
                .HasKey(w => (new { w.UserId, w.MovieId }));

            modelBuilder.Entity<Models.WatchList>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchLists)
                .HasForeignKey(w => w.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.WatchList>()
                .HasOne(w => w.Movie)
                .WithMany(s => s.WatchLists)
                .HasForeignKey( w => w.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            //MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(a => new { a.MovieId, a.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Movie)
                .WithMany(s => s.MovieActors)
                .HasForeignKey(a => a.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(a => a.ActorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //Unique index
            modelBuilder.Entity<MovieActor>()
                .HasIndex(s => new { s.ActorId, s.MovieId })
                .IsUnique(true)
                .HasName("UQ_MovieActor_ActorId_MovieId");


            //MovierGenre
            modelBuilder.Entity<MovieGenre>()
                .HasKey(g => new { g.MovieId, g.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(g => g.Movie)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(g => g.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(g => g.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(g => g.GenreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //Unique index
            modelBuilder.Entity<MovieGenre>()
                .HasIndex(s => new { s.GenreId, s.MovieId })
                .IsUnique(true)
                .HasName("UQ_MovieGenre_GenreId_MovieId");
        }
    }
}
