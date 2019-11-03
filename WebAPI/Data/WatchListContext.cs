using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WatchListContext : DbContext
    {
        public WatchListContext(DbContextOptions<WatchListContext> options) : 
            base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SerieMovie> SerieMovies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<SerieMovieGenre> SerieMovieGenres { get; set; }
        public DbSet<SerieMovieActor> SerieMovieActors { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User - Unique Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Username } )
                .IsUnique(true)
                .HasName("UQ_User_Username");

            //SerieMovie - Unique Indexes
            modelBuilder.Entity<SerieMovie>()
                .HasIndex(s => new { s.Name })
                .IsUnique(true)
                .HasName("UQ_SerieMovie_Name");

            //Genre - Unique Indexes
            modelBuilder.Entity<Genre>()
                .HasIndex(g => new { g.GenreName })
                .IsUnique(true)
                .HasName("UQ_Genre_Name");

            //SerieMovie - Unique Indexes
            modelBuilder.Entity<Actor>()
                .HasIndex(a => new { a.FullName })
                .IsUnique(true)
                .HasName("UQ_Actor_Name");

            //Watchlist
            modelBuilder.Entity<WatchList>()
                .HasKey(w => new { w.UserId, w.SerieMovieId });

            modelBuilder.Entity<WatchList>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchLists)
                .HasForeignKey(w => w.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchList>()
                .HasOne(w => w.SerieMovie)
                .WithMany(s => s.WatchLists)
                .HasForeignKey(w => w.SerieMovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            //SerieMovieActor
            modelBuilder.Entity<SerieMovieActor>()
                .HasKey(a => new { a.SerieMovieId, a.ActorId });

            modelBuilder.Entity<SerieMovieActor>()
                .HasOne(a => a.SerieMovie)
                .WithMany(s => s.SerieMovieActors)
                .HasForeignKey(a => a.SerieMovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SerieMovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(a => a.SerieMovieActors)
                .HasForeignKey(a => a.ActorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //Unique index
            modelBuilder.Entity<SerieMovieActor>()
                .HasIndex(s => new { s.ActorId, s.SerieMovieId })
                .IsUnique(true)
                .HasName("UQ_SerieMovieActor_ActorId_SerieMovieId");


            //SerieMovierGenre
            modelBuilder.Entity<SerieMovieGenre>()
                .HasKey(g => new { g.SerieMovieId, g.GenreId });

            modelBuilder.Entity<SerieMovieGenre>()
                .HasOne(g => g.SerieMovie)
                .WithMany(s => s.SerieMovieGenres)
                .HasForeignKey(g => g.SerieMovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SerieMovieGenre>()
                .HasOne(g => g.Genre)
                .WithMany(g => g.SerieMovieGenres)
                .HasForeignKey(g => g.GenreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //Unique index
            modelBuilder.Entity<SerieMovieGenre>()
                .HasIndex(s => new { s.GenreId, s.SerieMovieId })
                .IsUnique(true)
                .HasName("UQ_SerieMovieGenre_GenreId_SerieMovieId");


            //UserFriends
            modelBuilder.Entity<UserFriend>()
                .HasKey(u => new { u.UserId, u.FriendId });

            modelBuilder.Entity<UserFriend>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserFriends)
                .HasForeignKey(u => u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne(u => u.Friend)
                .WithMany(u => u.FriendUsers)
                .HasForeignKey(u => u.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
