using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Entities;

public partial class StudentManagementContext : DbContext
{
    public StudentManagementContext()
    {
    }

    public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC074AFD7ED1");

            entity.ToTable("Room");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC07F8B802DF");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "UQ__Student__A9D10534CA38C729").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Room).WithMany(p => p.Students)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__RoomId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
