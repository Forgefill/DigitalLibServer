using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DAL.Entities;

namespace DAL.Data
{
    public class LibDbContext : DbContext
    {

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<BookGenre> BookGenres { get; set; }

        public virtual DbSet<Chapter> Chapters { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        IConfiguration _configuration { get; set; }

        public LibDbContext(DbContextOptions<LibDbContext> options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQLSERVER"));
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
                entity.HasOne(i => i.Image).WithOne(b => b.Book).HasForeignKey<Image>(i => i.BookId);
                entity.HasIndex(t=>t.Title).IsUnique();
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasOne(a => a.Book).WithMany(c => c.Chapters).HasForeignKey(f => f.BookId);
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasOne(a => a.Book).WithMany(t => t.BookGenres).HasForeignKey(f => f.BookId);
                entity.HasOne(a => a.Genre).WithMany(t => t.BookGenres).HasForeignKey(f => f.GenreId);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(x => x.User).WithMany(c => c.Comments).HasForeignKey(f => f.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Chapter).WithMany(c => c.Comments).HasForeignKey(f => f.ChapterId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(x => x.User).WithMany(c => c.Reviews).HasForeignKey(f => f.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Book).WithMany(c => c.Reviews).HasForeignKey(f => f.BookId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
