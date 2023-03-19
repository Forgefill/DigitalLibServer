﻿using Microsoft.EntityFrameworkCore;
using DAL.Model.Entities;
using Microsoft.Extensions.Configuration;

namespace DAL.Data
{
    public class LibDbContext : DbContext
    {

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<BookTag> BookTags { get; set; }

        public virtual DbSet<BookGenre> BookGenres { get; set; }

        public virtual DbSet<Chapter> Chapters { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        IConfiguration _configuration { get; set; }

        public LibDbContext(DbContextOptions<LibDbContext> options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseOracle(_configuration.GetConnectionString("OracleLocal"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(a => a.Author).WithMany(a => a.Books).HasForeignKey(s => s.AuthorId);
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasOne(a => a.Book).WithMany(c => c.Chapters).HasForeignKey(f => f.BookId);
            });

            modelBuilder.Entity<BookTag>(entity =>
            {
                entity.HasOne(a => a.Book).WithMany(t => t.BookTags).HasForeignKey(f => f.BookId);
                entity.HasOne(a => a.Tag).WithMany(t => t.BookTags).HasForeignKey(f => f.TagId);
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasOne(a => a.Book).WithMany(t => t.BookGenres).HasForeignKey(f => f.BookId);
                entity.HasOne(a => a.Genre).WithMany(t => t.BookGenres).HasForeignKey(f => f.GenreId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(x => x.User).WithMany(c => c.Comments).HasForeignKey(f => f.UserId).IsRequired();
                entity.HasOne(x => x.Chapter).WithMany(c => c.Comments).HasForeignKey(f => f.ChapterId).IsRequired();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(x => x.User).WithMany(c => c.Reviews).HasForeignKey(f => f.UserId).IsRequired();
                entity.HasOne(x => x.Book).WithMany(c => c.Reviews).HasForeignKey(f => f.BookId).IsRequired();
            });
        }
    }
}
