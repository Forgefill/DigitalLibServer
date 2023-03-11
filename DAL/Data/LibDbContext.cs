using Microsoft.EntityFrameworkCore;
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
                                entity.HasKey(x => x.Id);
                                entity.Property(p => p.Login).IsRequired().HasColumnType("nvarchar2(50)");
                                entity.Property(p => p.Password).IsRequired().HasColumnType("nvarchar2(100)");
                                entity.Property(p => p.Role).IsRequired().HasColumnType("nvarchar2(14)");
                                entity.HasIndex(e => e.Login).IsUnique();
                                entity.HasIndex(e => e.Password).IsUnique();
                            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Title).IsRequired().HasColumnType("nvarchar2(100)");
                entity.Property(p => p.Description).IsRequired().HasColumnType("nvarchar2(1000)");
                entity.HasOne(a => a.Author).WithMany(a => a.Books).HasForeignKey(s => s.AuthorId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Name).IsRequired().HasColumnType("nvarchar2(50)");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Name).IsRequired().HasColumnType("nvarchar2(50)");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Title).IsRequired().HasColumnType("nvarchar2(100)");
                entity.Property(a => a.Content).HasColumnType("NCLOB");
                entity.Property(a => a.ChapterNum).IsRequired();
                entity.HasOne(a => a.Book).WithMany(c => c.Chapters).HasForeignKey(f => f.BookId);
            });

            modelBuilder.Entity<BookTag>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.TagId).IsRequired();
                entity.Property(p => p.BookId).IsRequired();
                entity.HasOne(a => a.Book).WithMany(t => t.BookTags).HasForeignKey(f => f.BookId);
                entity.HasOne(a => a.Tag).WithMany(t => t.BookTags).HasForeignKey(f => f.TagId);
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.GenreId).IsRequired();
                entity.Property(p => p.BookId).IsRequired();
                entity.HasOne(a => a.Book).WithMany(t => t.BookGenres).HasForeignKey(f => f.BookId);
                entity.HasOne(a => a.Genre).WithMany(t => t.BookGenres).HasForeignKey(f => f.GenreId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Likes).HasDefaultValue(0);
                entity.Property(p => p.Content).IsRequired().HasColumnType("nvarchar2(500)");
                entity.HasOne(x => x.User).WithMany(c => c.Comments).HasForeignKey(f => f.UserId).IsRequired();
                entity.HasOne(x => x.Chapter).WithMany(c => c.Comments).HasForeignKey(f => f.ChapterId).IsRequired();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(p => p.Likes).HasDefaultValue(0);
                entity.Property(p => p.Score).HasDefaultValue(5).IsRequired();
                entity.Property(p => p.Content).IsRequired().HasColumnType("NCLOB");
                entity.HasOne(x => x.User).WithMany(c => c.Reviews).HasForeignKey(f => f.UserId).IsRequired();
                entity.HasOne(x => x.Book).WithMany(c => c.Reviews).HasForeignKey(f => f.BookId).IsRequired();
            });
        }


    }
}
