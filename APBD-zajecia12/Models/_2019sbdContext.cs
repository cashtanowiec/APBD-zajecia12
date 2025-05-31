using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APBD_zajecia12.Models;

public partial class _2019sbdContext : DbContext
{
    public _2019sbdContext()
    {
    }

    public _2019sbdContext(DbContextOptions<_2019sbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("s30635");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_pk");

            entity.ToTable("Client");

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("Client_Trip_pk");

            entity.ToTable("Client_Trip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("Country_pk");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("Country_Trip_pk");
                        j.ToTable("Country_Trip");
                    });
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("Trip_pk");

            entity.ToTable("Trip");

            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });
        
        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                IdClient = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@example.com",
                Telephone = "123-456-789",
                Pesel = "12345678901"
            },
            new Client
            {
                IdClient = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna.nowak@example.com",
                Telephone = "987-654-321",
                Pesel = "10987654321"
            }
        );

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                IdTrip = 1,
                Name = "Wakacje w Grecji",
                Description = "Wypoczynek na plaży",
                DateFrom = new DateTime(2024, 07, 01),
                DateTo = new DateTime(2024, 07, 14),
                MaxPeople = 20
            },
            new Trip
            {
                IdTrip = 2,
                Name = "Wycieczka do Paryża",
                Description = "Zwiedzanie miasta światła",
                DateFrom = new DateTime(2024, 09, 10),
                DateTo = new DateTime(2024, 09, 17),
                MaxPeople = 15
            }
        );

        modelBuilder.Entity<ClientTrip>().HasData(
            new ClientTrip
            {
                IdClient = 1,
                IdTrip = 1,
                RegisteredAt = new DateTime(2024, 06, 01),
                PaymentDate = new DateTime(2024, 06, 15)
            },
            new ClientTrip
            {
                IdClient = 2,
                IdTrip = 2,
                RegisteredAt = new DateTime(2024, 08, 01),
                PaymentDate = null
            }
        );

        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                IdCountry = 1,
                Name = "Grecja"
            },
            new Country
            {
                IdCountry = 2,
                Name = "Francja"
            }
        );

        modelBuilder.Entity("CountryTrip").HasData(
            new { IdCountry = 1, IdTrip = 1 },
            new { IdCountry = 2, IdTrip = 2 }
        );
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
