using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hoa123.Models
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Show> Shows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost; database=Cinema;user=sa;password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Account__C9F2845750F71E6F");

                entity.ToTable("Account");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.SeatStatus)
                    .HasMaxLength(1000)
                    .IsFixedLength(true);

                entity.Property(e => e.ShowId).HasColumnName("ShowID");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Shows");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode)
                    .HasName("PK_Country");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.CountryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.FilmId).HasColumnName("FilmID");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Films_Countries");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Films_Genres");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Show>(entity =>
            {
                entity.Property(e => e.ShowId).HasColumnName("ShowID");

                entity.Property(e => e.FilmId).HasColumnName("FilmID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.ShowDate).HasColumnType("date");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shows_Films");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shows_Rooms");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
