using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_SASF.Entities;

public partial class ZoologicoContext : DbContext
{
    public ZoologicoContext()
    {
    }

    public ZoologicoContext(DbContextOptions<ZoologicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animale> Animales { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<ZoologicoSecuenciasPrimaria> ZoologicoSecuenciasPrimarias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database = zoologico; Integrated Security = True; TrustServerCertificate=True;");
        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__animales__3213E83F8B5E5737");

            entity.ToTable("animales");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Especie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("especie");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaEstado)
                .HasColumnType("date")
                .HasColumnName("fecha_estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("date")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.ObservacionEstado)
                .HasMaxLength(2000)
                .HasColumnName("observacion_estado");
            entity.Property(e => e.RazaId).HasColumnName("raza_id");
            entity.Property(e => e.UbicacionIngreso)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_ingreso");
            entity.Property(e => e.UbicacionModificacion)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_modificacion");
            entity.Property(e => e.UsuarioIngreso).HasColumnName("usuario_ingreso");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");

            entity.HasOne(d => d.Raza).WithMany(p => p.Animales)
                .HasForeignKey(d => d.RazaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__animales__raza_i__4F7CD00D");
        });

        modelBuilder.Entity<Raza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__razas__3213E83F70EFD905");

            entity.ToTable("razas");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaEstado)
                .HasColumnType("date")
                .HasColumnName("fecha_estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("date")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.ObservacionEstado)
                .HasMaxLength(2000)
                .HasColumnName("observacion_estado");
            entity.Property(e => e.OrigenGeografico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("origen_geografico");
            entity.Property(e => e.UbicacionIngreso)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_ingreso");
            entity.Property(e => e.UbicacionModificacion)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_modificacion");
            entity.Property(e => e.UsuarioIngreso).HasColumnName("usuario_ingreso");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
        });

        modelBuilder.Entity<ZoologicoSecuenciasPrimaria>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__zoologic__40F9A207144B783E");

            entity.ToTable("zoologico_secuencias_primarias");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaEstado)
                .HasColumnType("date")
                .HasColumnName("fecha_estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("date")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IncrementaEn)
                .HasDefaultValueSql("((1))")
                .HasColumnName("incrementa_en");
            entity.Property(e => e.ObservacionEstado)
                .HasMaxLength(2000)
                .HasColumnName("observacion_estado");
            entity.Property(e => e.UbicacionIngreso)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_ingreso");
            entity.Property(e => e.UbicacionModificacion)
                .HasMaxLength(200)
                .HasColumnName("ubicacion_modificacion");
            entity.Property(e => e.UsuarioIngreso).HasColumnName("usuario_ingreso");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
            entity.Property(e => e.ValorActual).HasColumnName("valor_actual");
            entity.Property(e => e.ValorInicial).HasColumnName("valor_inicial");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
