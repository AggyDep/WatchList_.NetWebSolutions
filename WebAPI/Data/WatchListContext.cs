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
