using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiDemoHotel;

public partial class User15Context : DbContext
{
    public User15Context()
    {

    }

    public User15Context(DbContextOptions<User15Context> options)
        : base(options)
    {  }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.35;userid=user15;password=72925;database=user15;characterset=utfmb4", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Date>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdClient, "Dates_User_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DateCheckIn).HasColumnType("datetime");
            entity.Property(e => e.DateCheckOut).HasColumnType("datetime");
            entity.Property(e => e.IdClient)
                .HasColumnType("int(11)")
                .HasColumnName("idClient");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Dates)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Dates_User_FK");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Room");

            entity.HasIndex(e => e.IdCategory, "Room_Category_FK");

            entity.HasIndex(e => e.IdStatus, "Room_Status_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Floor).HasColumnType("int(11)");
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.IdStatus)
                .HasColumnType("int(11)")
                .HasColumnName("idStatus");
            entity.Property(e => e.Number).HasColumnType("int(11)");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("Room_Category_FK");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("Room_Status_FK");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Status");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.IdRole, "User_Role_FK");

            entity.HasIndex(e => e.IdRoom, "User_Room_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdRole)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)")
                .HasColumnName("idRole");
            entity.Property(e => e.IdRoom)
                .HasColumnType("int(11)")
                .HasColumnName("idRoom");
            entity.Property(e => e.LastLogInDate).HasColumnType("datetime");
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Role_FK");

            entity.HasOne(d => d.IdRoomNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRoom)
                .HasConstraintName("User_Room_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
