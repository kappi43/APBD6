using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApplication11.Services
{
    public class DbService : IDbService
    {
        MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddDoctor(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            return _dbContext.SaveChanges() >0;
        }

        public Doctor GetDoctor(int id)
        {
            return _dbContext.Doctors.Find(id);
        }

        public SomePrescription GetPrescription(int id)
        {
            SomePrescription somePrescription = new();
            var ls = _dbContext.Prescription_Medicaments.Where(p => p.IdPrescription == id);
            foreach (var p in ls)
            {
                somePrescription.MedicamentsIds.AddLast(p.IdPrescription);
            }
            var presc = _dbContext.Prescriptions.Include(x => x.Patient).Include(x => x.Doctor).Where(e => e.IdPrescription == id).FirstOrDefault(); ;
            somePrescription.PatientName = presc.Patient.FirstName;
            somePrescription.PatientLastName = presc.Patient.LastName;
            somePrescription.PatientBirthDate = presc.Patient.BirthDate;
            somePrescription.DoctorName = presc.Doctor.FirstName;
            somePrescription.DoctorLastName = presc.Doctor.LastName;
            somePrescription.DoctorEmail = presc.Doctor.Email;
            return somePrescription;
        }

        public bool ModifyDoctor(int id, Doctor doctor)
        {
            var found = _dbContext.Doctors.Find(id);
            if (found != null)
            {
                found.FirstName = doctor.FirstName;
                found.LastName = doctor.LastName;
                found.Email = doctor.Email;
                _dbContext.SaveChanges();
                return true;
            }
            return false;

        }

        public bool RemoveDoctor(int id)
        {
            if (_dbContext.Doctors.Find(id)!= null)
            {
                _dbContext.Remove(_dbContext.Doctors.Find(id));
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
