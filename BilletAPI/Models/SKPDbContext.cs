using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MySql.EntityFrameworkCore;
#nullable disable

namespace BilletAPI.Models
{
    public partial class SKPDbContext : DbContext
    {
        private readonly IConfiguration Configuration;
        
        public SKPDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SKPDbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ZipCode> ZipCodes { get; set; }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectionString = Configuration.GetConnectionString("Conn");

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString, o => { o.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null); });
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptions => mySqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null));
                //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.EventZipCode, "eventZipCode");

                entity.Property(e => e.EventId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eventId");

                entity.Property(e => e.EventDate)
                    .HasColumnType("date")
                    .HasColumnName("eventDate");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("eventName");

                entity.Property(e => e.EventStrtName)
                    .IsRequired()
                    .HasColumnName("eventStrtName");

                entity.Property(e => e.EventTime).HasColumnName("eventTime");

                entity.Property(e => e.EventZipCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("eventZipCode");

                entity.HasOne(d => d.EventZipCodeNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventZipCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Events_ibfk_1");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
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
                    .HasConstraintName("Tickets_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Tickets_ibfk_2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.HasKey(e => e.PostalCode)
                    .HasName("PRIMARY");

                entity.ToTable("ZipCode");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("postalCode");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasColumnName("city");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
