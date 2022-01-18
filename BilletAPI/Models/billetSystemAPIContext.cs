using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BilletAPI.Models
{
    public partial class BilletSystemAPIContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public BilletSystemAPIContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public BilletSystemAPIContext(DbContextOptions<DbContext> options)
          : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ZipCode> ZipCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = Configuration.GetConnectionString("Conn");

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql("server=51.195.42.8;port=3306;database=billetSystemAPI;user=Lazirr;password=Wkt67ssv!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.5-mariadb"));
                optionsBuilder.UseMySql(conn, MariaDbServerVersion.AutoDetect(conn));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.HasIndex(e => e.EventZipCode, "eventZipCode");

                entity.Property(e => e.EventId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eventId");

                entity.Property(e => e.EventDate)
                    .HasColumnType("date")
                    .HasColumnName("eventDate");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("eventName");

                entity.Property(e => e.EventStrtName)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("eventStrtName");

                entity.Property(e => e.EventTime)
                    .HasColumnType("time")
                    .HasColumnName("eventTime");

                entity.Property(e => e.EventZipCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("eventZipCode");

                entity.HasOne(d => d.EventZipCodeNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventZipCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_ibfk_1");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("ticket");

                entity.HasIndex(e => e.EventId, "eventId");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.TicketId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ticketId");

                entity.Property(e => e.EventId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eventId");

                entity.Property(e => e.IsUsed).HasColumnName("isUsed");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_ibfk_2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.HasKey(e => e.PostalCode)
                    .HasName("PRIMARY");

                entity.ToTable("ZipCode");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("postalCode");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("city");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
