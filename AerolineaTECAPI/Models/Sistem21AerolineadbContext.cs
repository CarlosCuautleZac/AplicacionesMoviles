﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AerolineaTECAPI.Models;

public partial class Sistem21AerolineadbContext : DbContext
{
    public Sistem21AerolineadbContext()
    {
    }

    public Sistem21AerolineadbContext(DbContextOptions<Sistem21AerolineadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Vuelo> Vuelo { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vuelo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Destino)
                .HasMaxLength(100)
                .HasColumnName("destino");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Numerovuelo)
                .HasMaxLength(10)
                .HasColumnName("numerovuelo");
            entity.Property(e => e.Puerta)
                .HasColumnType("int(11)")
                .HasColumnName("puerta");
            entity.Property(e => e.UltimaEdicionFecha)
                .HasColumnType("datetime")
                .HasColumnName("ultimaEdicionFecha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
