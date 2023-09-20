using System;
using System.Collections.Generic;
using Devsu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public partial class DevsuContext : DbContext
{
    public DevsuContext()
    {
    }

    public DevsuContext(DbContextOptions<DevsuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

//    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cliente");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(500);
                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.Genero).HasColumnName("genero").HasMaxLength(20);
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.ToTable("cuenta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(13, 4)")
                .HasColumnName("saldo_inicial");
            entity.Property(e => e.TipoCuenta).HasColumnName("tipo_cuenta")
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuentas)
               .HasForeignKey(d => d.ClienteId)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_cuenta_cliente");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("movimiento");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(13, 4)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoMovimiento).HasColumnName("tipo_movimiento");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(13, 4)")
                .HasColumnName("valor");
            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_movimiento_cuenta");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
