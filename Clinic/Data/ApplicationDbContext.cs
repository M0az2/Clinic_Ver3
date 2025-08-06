using Clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Clinic.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Tables
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9OGSSGC\\SQLEXPRESS;Database=rr;User ID=sa;Password=12345;Encrypt=True;TrustServerCertificate=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add unique constraint to prevent duplicate appointments for the same doctor at the same time
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.DoctorId, a.AppointmentDate })
                .IsUnique();
            modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FullName = "Admin User",
                Email = "admin@admin.com",
                Password = "admin123", 
                Specialization = "Admin"
            }
            );

        }

    }
}
