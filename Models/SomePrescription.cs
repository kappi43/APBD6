using System;
using System.Collections.Generic;

namespace WebApplication11.Models
{
    public class SomePrescription
    {
        public SomePrescription()
        {
            MedicamentsIds = new();
        }

        public int IdPrescription { get; set; }
        public LinkedList<int> MedicamentsIds { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }

    }
}
