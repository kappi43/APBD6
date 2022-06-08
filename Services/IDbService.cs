using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Models;

namespace WebApplication11.Services
{
    public interface IDbService
    {
        Doctor GetDoctor(int id);
        bool RemoveDoctor(int id);
        bool AddDoctor(Doctor trip);
        bool ModifyDoctor(int id, Doctor clientTrip);
        SomePrescription GetPrescription(int id);
    }
}
