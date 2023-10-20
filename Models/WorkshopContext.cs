using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkshopAPI.Models;

public partial class WorkshopContext : DbContext
{
    public WorkshopContext()
    {
    }

    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventGender> EventGenders { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<SportCategory> SportCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGender> UserGenders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event");

            entity.HasIndex(e => e.IdEventGender, "event_event_gender2_FK");

            entity.HasIndex(e => e.IdLevel, "event_level1_FK");

            entity.HasIndex(e => e.IdSport, "event_sport0_FK");

            entity.HasIndex(e => e.IdUser, "event_user_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IdEventGender)
                .HasColumnType("int(11)")
                .HasColumnName("id_event_gender");
            entity.Property(e => e.IdLevel)
                .HasColumnType("int(11)")
                .HasColumnName("id_level");
            entity.Property(e => e.IdSport)
                .HasColumnType("int(11)")
                .HasColumnName("id_sport");
            entity.Property(e => e.IdUser)
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.NbPeople)
                .HasColumnType("int(11)")
                .HasColumnName("nbPeople");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");

            entity.HasOne(d => d.IdEventGenderNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdEventGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_event_gender2_FK");

            entity.HasOne(d => d.IdLevelNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_level1_FK");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdSport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_sport0_FK");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_user_FK");
        });

        modelBuilder.Entity<EventGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event_gender");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("level");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sport");

            entity.HasIndex(e => e.IdSportCategory, "sport_sport_category_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdSportCategory)
                .HasColumnType("int(11)")
                .HasColumnName("id_sport_category");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.IdSportCategoryNavigation).WithMany(p => p.Sports)
                .HasForeignKey(d => d.IdSportCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sport_sport_category_FK");
        });

        modelBuilder.Entity<SportCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sport_category");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdUserGender, "user_user_gender_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Expert).HasColumnName("expert");
            entity.Property(e => e.Firstname)
                .HasMaxLength(200)
                .HasColumnName("firstname");
            entity.Property(e => e.IdUserGender)
                .HasColumnType("int(11)")
                .HasColumnName("id_user_gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(200)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasOne(d => d.IdUserGenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdUserGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_user_gender_FK");

            entity.HasMany(d => d.IdEvents).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "Participate",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("IdEvent")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("participate_event0_FK"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("participate_user_FK"),
                    j =>
                    {
                        j.HasKey("Id", "IdEvent")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("participate");
                        j.HasIndex(new[] { "IdEvent" }, "participate_event0_FK");
                        j.IndexerProperty<int>("Id")
                            .HasColumnType("int(11)")
                            .HasColumnName("id");
                        j.IndexerProperty<int>("IdEvent")
                            .HasColumnType("int(11)")
                            .HasColumnName("id_event");
                    });
        });

        modelBuilder.Entity<UserGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_gender");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
