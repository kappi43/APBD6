using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication11.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext( DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired();
                p.HasMany(e => e.Prescriptions).WithOne(e => e.Patient);
                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-02-01")},
                    new Patient { IdPatient = 2, FirstName = "Adam", LastName = "Kowalski", BirthDate = DateTime.Parse("2002-02-01") }
                );
            });
            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);
                d.HasMany(e => e.Prescriptions).WithOne(e => e.Doctor);

                d.HasData(
                   new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "email@email.com" },
                   new Doctor { IdDoctor = 2, FirstName = "Adam", LastName = "Kowalski", Email = "email@email.com" }
               );
            });
            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Now , DueDate = DateTime.Parse("2022-07-07"), IdDoctor = 1, IdPatient = 1},
                    new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Parse("2023-07-07"), IdDoctor = 2, IdPatient = 2 }
                );
            });
            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);
                m.HasMany(e => e.Prescription_Medicament).WithOne(e => e.Medicament);

                m.HasData(
                    new Medicament { IdMedicament = 1, Type = "Antydepresant", Description = "Inhibitor wychwytu zwrotnego serotoniny", Name = "Dulsevia"},
                    new Medicament { IdMedicament = 2, Type = "Suplement diety", Description = "kwasy tłuszczowe omega-3 i omega-6", Name = "Neogladyna" }
                );
            });
            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasKey(e => new { e.IdPrescription, e.IdMedicament});
                pm.Property(e => e.Dose);
                pm.Property(e => e.Details);
                pm.HasOne(e => e.Prescription).WithMany(p => p.Prescription_Medicament).HasForeignKey(e => e.IdPrescription);
                pm.HasOne(e => e.Medicament).WithMany(p => p.Prescription_Medicament).HasForeignKey(e => e.IdMedicament);

                pm.HasData(
                    new Prescription_Medicament { IdPrescription = 1, IdMedicament=1, Details="Przyjmowac raz dziennie rano z posilkiem", Dose=1},
                    new Prescription_Medicament { IdPrescription = 2, IdMedicament = 2, Details = "Przyjmowac raz dziennie rano z posilkiem", Dose = 2 }
                );
            }
            );
        }
    }
}
