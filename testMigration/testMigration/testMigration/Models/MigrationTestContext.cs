using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace testMigration.Models;

public partial class MigrationTestContext : DbContext
{
    public MigrationTestContext()
    {
    }

    public MigrationTestContext(DbContextOptions<MigrationTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Salon> Salons { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("db_accessadmin");

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.IdMessage).HasName("Message_PK");

            entity.ToTable("Message", "dbo");

            entity.Property(e => e.IdMessage).HasColumnName("id_message");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdChannel).HasColumnName("id_channel");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Message1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("message");

            entity.HasOne(d => d.IdChannelNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdChannel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Salon0_FK");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Utilisateur_FK");
        });

        modelBuilder.Entity<Salon>(entity =>
        {
            entity.HasKey(e => e.IdChannel).HasName("Salon_PK");

            entity.ToTable("Salon", "dbo");

            entity.Property(e => e.IdChannel).HasColumnName("id_channel");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Salons)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Salon_Utilisateur_FK");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Utilisateur_PK");

            entity.ToTable("Utilisateur", "dbo");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pseudo");
            entity.Property(e => e.Theme).HasColumnName("theme");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
