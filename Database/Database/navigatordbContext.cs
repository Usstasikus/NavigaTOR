using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Database
{
    public partial class navigatordbContext : DbContext
    {
        public static IConfigurationRoot Configuration { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<RoutePlace> RoutePlaces { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<UserPlace> UserPlaces { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=navigatordb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Feedbacks_Places");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_Feedbacks_Routes");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.Coordinates).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<RoutePlace>(entity =>
            {
                entity.HasKey(e => new { e.RouteId, e.PlaceId });

                entity.ToTable("Route_Places");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.RoutePlaces)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_Places_Places");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RoutePlaces)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_Places_Routes");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.Access).HasMaxLength(64);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Users");
            });

            modelBuilder.Entity<UserPlace>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PlaceId });

                entity.ToTable("User_Places");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.UserPlaces)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Places_Places");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlaces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Places_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Surname).HasMaxLength(64);
            });
        }
    }
}
