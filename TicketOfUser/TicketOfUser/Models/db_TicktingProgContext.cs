using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class db_TicktingProgContext : IdentityDbContext
    {
        public db_TicktingProgContext()
        {
        }

        public db_TicktingProgContext(DbContextOptions<db_TicktingProgContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketToDeveloper> TicketToDevelopers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=contr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AspNetUsersId).HasMaxLength(450);

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("Comment");

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AspNetUsersId)
                    .HasConstraintName("FK__Comments__AspNet__5535A963");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__Comments__Ticket__18EBB532");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status1)
                    .IsRequired()
                    .HasColumnName("Status");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.AspNetUsersId).HasMaxLength(450);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.AspNetUsersId)
                    .HasConstraintName("FK__Ticket__AspNetUs__17F790F9");

                entity.HasOne(d => d.Prority)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ProrityId)
                    .HasConstraintName("FK__Ticket__ProrityI__17036CC0");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Ticket__StatusId__160F4887");
            });

            modelBuilder.Entity<TicketToDeveloper>(entity =>
            {
                entity.HasKey(e => new { e.DeveloperId, e.TicketId })
                    .HasName("PK__TicketTo__B91A8091593B3FC4");

                entity.ToTable("TicketToDeveloper");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.TicketToDevelopers)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TicketToD__Devel__2BFE89A6");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketToDevelopers)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TicketToD__Ticke__2CF2ADDF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
