using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;

namespace LogMessageTest;

public partial class ProceedxContext : DbContext
{
    public ProceedxContext()
    {
    }

    public ProceedxContext(DbContextOptions<ProceedxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Logmessage> Logmessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=proceedx;Username=postgres;Password=123123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<loglevel>(name: "loglevel", nameTranslator: new NpgsqlNullNameTranslator());

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_pkey");

            entity.ToTable("application");

            entity.HasIndex(e => e.Name, "idx_application_name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Logmessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logmessage_pkey");

            entity.ToTable("logmessage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("application_id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Message).HasColumnName("message");

            entity.HasOne(d => d.Application).WithMany(p => p.Logmessages)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logmessage_application_id_fkey");

            entity.Property(d => d.log_level)
                .HasColumnName("log_level")
                .HasColumnType("loglevel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
