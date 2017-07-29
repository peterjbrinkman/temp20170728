using Microsoft.EntityFrameworkCore;

namespace MovieBook.Models
{
    public partial class MovieBookDBContext : DbContext
    {
        public virtual DbSet<ActorEntities> ActorEntities { get; set; }
        public virtual DbSet<GenreEntities> GenreEntities { get; set; }
        public virtual DbSet<MovieActor> MovieActor { get; set; }
        public virtual DbSet<MovieEntities> MovieEntities { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<RatingEntities> RatingEntities { get; set; }

        public MovieBookDBContext(DbContextOptions<MovieBookDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorEntities>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<GenreEntities>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.ActorId })
                    .HasName("PK_MovieActor");

                entity.Property(e => e.MovieId);

                entity.Property(e => e.ActorId);
            });

            modelBuilder.Entity<MovieEntities>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(1000); 

                entity.Property(e => e.RatingId);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId })
                    .HasName("PK_MovieGenre");

                entity.Property(e => e.MovieId);

                entity.Property(e => e.GenreId);
            });

            modelBuilder.Entity<RatingEntities>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(5);

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(1000);
            });
        }
    }
}