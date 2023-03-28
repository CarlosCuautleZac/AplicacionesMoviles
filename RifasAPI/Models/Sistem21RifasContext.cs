using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RifasAPI.Models;

public partial class Sistem21RifasContext : DbContext
{
    public Sistem21RifasContext()
    {
    }

    public Sistem21RifasContext(DbContextOptions<Sistem21RifasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boletos> Boletos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_rifas;username=sistem21_rifas;password=sistemas19_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Boletos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("boletos");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Eliminado).HasColumnType("bit(1)");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.NombrePersona).HasMaxLength(255);
            entity.Property(e => e.NumeroBoleto).HasColumnType("int(10) unsigned");
            entity.Property(e => e.NumeroTelefono).HasMaxLength(10);
            entity.Property(e => e.Pagado).HasColumnType("bit(1)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
