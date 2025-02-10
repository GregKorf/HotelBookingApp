using HotelBookingApp.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Data
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext()
        {

        }

        public HotelBookingDbContext(DbContextOptions options) : base(options) 
        {
        
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.Username).HasMaxLength(50);
                entity.Property(u => u.Password).HasMaxLength(80);
                entity.Property(u => u.Email).HasMaxLength(50);
                entity.Property(u => u.FirstName).HasMaxLength(255);
                entity.Property(u => u.LastName).HasMaxLength(255);
                entity.Property(u => u.UserRole).HasConversion<string>();

                entity.Property(u => u.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(u => u.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(u => u.Username, "IX_Users_Username").IsUnique();
                entity.HasIndex(u => u.Email, "IX_Users_Email").IsUnique();

            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins");
                
                entity.Property(a => a.JobDescription).HasMaxLength(100);
                entity.Property(a => a.PhoneNumber).HasMaxLength(255);


                entity.Property(a => a.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(a => a.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(a => a.JobDescription, "IX_Admins_JobDescription");
                entity.HasIndex(a => a.PhoneNumber, "IX_Admins_PhoneNumber").IsUnique();
                entity.HasIndex(a => a.UserId, "IX_Admins_UserId").IsUnique();

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(c => c.Address).HasMaxLength(255);
                entity.Property(c => c.City).HasMaxLength(255);
                entity.Property(c => c.PhoneNumber).HasMaxLength(255);

                entity.Property(c => c.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(c => c.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(c => c.PhoneNumber, "IX_Customers_PhoneNumber").IsUnique();
                entity.HasIndex(c => c.UserId, "IX_Customers_UserId").IsUnique();
            });


            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");

                entity.Property(b => b.CheckInDate).HasColumnType("datetime");
                entity.Property(b => b.CheckOutDate).HasColumnType("datetime");
                entity.Property(b => b.Status).HasDefaultValue(BookingStatus.Active).HasConversion<string>();

                entity.Property(b => b.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(b => b.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(b => b.CheckInDate, "IX_Bookings_CheckInDate");
                entity.HasIndex(b => b.CheckOutDate, "IX_Bookings_CheckOutDate");
                entity.HasIndex(b => b.Status, "IX_Bookings_Status");
                entity.HasIndex(b => b.CustomerId, "IX_Bookings_CustomerId");
                entity.HasIndex(b => b.RoomId, "IX_Bookings_RoomId");
                entity.HasIndex(b => b.AdminId, "IX_Bookings_AdminId");
            });


            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");

                entity.Property(r => r.RoomPricePerNight).HasColumnType("decimal(18,2)");
                entity.Property(r => r.RoomNumber).HasMaxLength(10);
                entity.Property(r => r.RoomType).HasConversion<string>();

                entity.Property(r => r.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(r => r.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(r => r.AdminId, "IX_Rooms_AdminId");
                entity.HasIndex(r => r.RoomNumber, "IX_Rooms_RoomNumber").IsUnique();
                entity.HasIndex(r => r.RoomPricePerNight, "IX_Rooms_RoomPricePerNight");
            });
        }
    }
}
