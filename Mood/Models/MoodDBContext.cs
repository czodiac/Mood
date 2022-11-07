using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mood.Models
{
    public partial class MoodDBContext : DbContext
    {
        public MoodDBContext(DbContextOptions<MoodDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblLocation> TblLocations { get; set; } = null!;
        public virtual DbSet<TblMood> TblMoods { get; set; } = null!;
        public virtual DbSet<TblMoodName> TblMoodNames { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("tblLocations");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.DistanceXaxis).HasColumnName("DistanceXAxis");

                entity.Property(e => e.DistanceYaxis).HasColumnName("DistanceYAxis");

                entity.Property(e => e.LocationName).HasMaxLength(100);
            });

            modelBuilder.Entity<TblMood>(entity =>
            {
                entity.ToTable("tblMoods");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.MoodId).HasColumnName("MoodID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblMoods)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMoods_tblLocations");

                entity.HasOne(d => d.Mood)
                    .WithMany(p => p.TblMoods)
                    .HasForeignKey(d => d.MoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMoods_tblMoodNames");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblMoods)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMoods_tblUsers");
            });

            modelBuilder.Entity<TblMoodName>(entity =>
            {
                entity.HasKey(e => e.MoodId);

                entity.ToTable("tblMoodNames");

                entity.Property(e => e.MoodId).HasColumnName("MoodID");

                entity.Property(e => e.MoodName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUsers");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
