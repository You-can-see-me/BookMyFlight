using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Admin.Service.EntityModels
{
    public partial class BookMyFlightDBContext : DbContext
    {
        public BookMyFlightDBContext()
        {
        }

        public BookMyFlightDBContext(DbContextOptions<BookMyFlightDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingHistory> BookingHistories { get; set; }
        public virtual DbSet<CancelHistory> CancelHistories { get; set; }
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }
        public virtual DbSet<FlightInventory> FlightInventories { get; set; }
        public virtual DbSet<LoginUser> LoginUsers { get; set; }
        public virtual DbSet<PassengerDetail> PassengerDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.ToTable("BookingHistory");

                entity.Property(e => e.BookedBy)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.BookedOn)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.BookingStatus).HasMaxLength(50);

                entity.Property(e => e.DiscountUsed).HasMaxLength(20);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Pnr)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("PNR");

                entity.Property(e => e.SeatType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("(N'Non-Business')");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingHistory_FlightInventory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingHistory_LoginUser");
            });

            modelBuilder.Entity<CancelHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CancelHistory");

                entity.Property(e => e.CancelledOn).HasColumnType("datetime");

                entity.Property(e => e.Pnr)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("PNR");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CancelHistory_LoginUser");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.Property(e => e.DiscountCode1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DiscountCode");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<FlightInventory>(entity =>
            {
                entity.ToTable("FlightInventory");

                entity.Property(e => e.AirlineLogo).HasColumnType("image");

                entity.Property(e => e.AirlineName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.AirlineStatus)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Departure)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.DestinationDateTime).HasColumnType("datetime");

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModelType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ScheduledDays)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TicketCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<LoginUser>(entity =>
            {
                entity.ToTable("LoginUser");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'U')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PassengerDetail>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SeatNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.BookingHistory)
                    .WithMany(p => p.PassengerDetails)
                    .HasForeignKey(d => d.BookingHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PassengerDetails_BookingHistory");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
