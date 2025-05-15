using Microsoft.EntityFrameworkCore;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Configuration.Interceptors;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static psymed_platform.Medication.Domain.Model.Aggregates.Medication;

namespace psymed_platform.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }

        public DbSet<Medication.Domain.Model.Aggregates.Medication> Medications { get; set; }
        public DbSet<psymed_platform.Appoiment_Administration.Domain.Models.Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddInterceptors(new CreatedUpdatedInterceptor());
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ValueConverter para MedicationName
            var medicationNameConverter = new ValueConverter<Medication.Domain.Model.ValueObjects.MedicationName, string>(
                v => v.Name,
                v => new Medication.Domain.Model.ValueObjects.MedicationName(v)
            );

            modelBuilder.Entity<Medication.Domain.Model.Aggregates.Medication>()
                .ToTable("medications")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Medication.Domain.Model.Aggregates.Medication>()
                .Property(m => m.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Medication.Domain.Model.Aggregates.Medication>()
                .Property(m => m.Name)
                .HasConversion(medicationNameConverter)
                .IsRequired();

            modelBuilder.Entity<Medication.Domain.Model.Aggregates.Medication>()
                .OwnsOne(m => m.LifeCycleMedication);

            modelBuilder.Entity<Medication.Domain.Model.Aggregates.Medication>()
                .OwnsOne(m => m.Prescription);

            

            modelBuilder.Entity<psymed_platform.Appoiment_Administration.Domain.Models.Appointment>()
                .ToTable("Appointments")
                .HasKey(a => a.Id);

            modelBuilder.Entity<psymed_platform.Appoiment_Administration.Domain.Models.Appointment>()
                .Property(a => a.Fecha)
                .HasColumnType("datetime");
        }
    }
}