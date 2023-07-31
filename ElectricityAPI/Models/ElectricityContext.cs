using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElectricityAPI.Models;

public partial class ElectricityContext : DbContext
{
    public ElectricityContext()
    {
    }

    public ElectricityContext(DbContextOptions<ElectricityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Governorate> Governorates { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Electricity;Trusted_Connection=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_100_CI_AI");

        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("Area");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Area1).HasColumnName("Area");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.TimeId).HasColumnName("TimeID");

            entity.HasOne(d => d.City).WithMany(p => p.Areas)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Area_City");

            entity.HasOne(d => d.Time).WithMany(p => p.Areas)
                .HasForeignKey(d => d.TimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Area_Area");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City1).HasColumnName("City");
            entity.Property(e => e.GovId).HasColumnName("GovID");

            entity.HasOne(d => d.Gov).WithMany(p => p.Cities)
                .HasForeignKey(d => d.GovId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Governorate");
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.ToTable("Governorate");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Hours");

            entity.ToTable("Time");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
