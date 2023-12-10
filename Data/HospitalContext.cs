using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HospitalSystem.Data
{
    public class HospitalContext: DbContext 
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var configSection = configBuilder.GetSection("ConnectionStrings");
            var connectionString = configSection["SQLServerConnection"] ?? null;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
             .HasMany(patient => patient.Appointments)
             .WithOne(appointment => appointment.Patient)
             .HasForeignKey(appointment => appointment.PatientID);

            modelBuilder.Entity<Doctor>()
                .HasMany(doctor => doctor.Appointments)
                .WithOne(appointment => appointment.Doctor)
                .HasForeignKey(appointment => appointment.DoctorID);

            modelBuilder.Entity<Room>()
                .HasMany(room => room.Patients)
                .WithOne(patient => patient.Room)
                .HasForeignKey(patient => patient.RoomID);
        }

        public DbSet<Patient> Patients { get; set; }    
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Room> Rooms { get; set; }  
    }
}
