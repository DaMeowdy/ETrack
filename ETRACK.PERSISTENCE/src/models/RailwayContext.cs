using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using src.models;
namespace src.models;

public partial class RailwayContext : DbContext
{
    private readonly IConfigurationRoot _configurationRoot;
    public RailwayContext()
    {
        _configurationRoot = new ConfigurationBuilder().AddUserSecrets<RailwayContext>().Build();
    }

    public RailwayContext(DbContextOptions<RailwayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dosage> Dosages { get; set; }

    public virtual DbSet<Dose> Doses { get; set; }

    public virtual DbSet<EstrogenLevel> EstrogenLevels { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configurationRoot["connSTR"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dosage>(entity =>
        {
            entity.HasKey(e => e.DosageId).HasName("dosages_pkey");

            entity.ToTable("dosages");

            entity.Property(e => e.DosageId).HasColumnName("dosage_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Concentration).HasColumnName("concentration");
            entity.Property(e => e.Ester).HasColumnName("ester");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Dosages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("dosages_user_id_fkey");
        });

        modelBuilder.Entity<Dose>(entity =>
        {
            entity.HasKey(e => e.DoseId).HasName("doses_pkey");

            entity.ToTable("doses");

            entity.Property(e => e.DoseId).HasColumnName("dose_id");
            entity.Property(e => e.DateScheduled).HasColumnName("date_scheduled");
            entity.Property(e => e.DosageId).HasColumnName("dosage_id");
            entity.Property(e => e.IsDone)
                .HasDefaultValue(false)
                .HasColumnName("is_done");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Dosage).WithMany(p => p.Doses)
                .HasForeignKey(d => d.DosageId)
                .HasConstraintName("doses_dosage_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Doses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("doses_user_id_fkey");
        });

        modelBuilder.Entity<EstrogenLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("estrogen_levels_pkey");

            entity.ToTable("estrogen_levels");

            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.DateTested).HasColumnName("date_tested");
            entity.Property(e => e.LevelPgml).HasColumnName("level_pgml");
            entity.Property(e => e.LevelPmol).HasColumnName("level_pmol");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EstrogenLevels)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("estrogen_levels_user_id_fkey");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("logins_pkey");

            entity.ToTable("logins");

            entity.HasIndex(e => e.UserId, "logins_user_id_key").IsUnique();

            entity.HasIndex(e => e.Username, "logins_username_key").IsUnique();

            entity.Property(e => e.LoginId).HasColumnName("login_id");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.User).WithOne(p => p.Login)
                .HasForeignKey<Login>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("logins_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PreferredMeasurement).HasColumnName("preferred_measurement");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
