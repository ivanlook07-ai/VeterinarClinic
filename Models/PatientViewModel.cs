using System.Collections.Generic;

namespace VeterinaryClinic.Models
{
    public class PatientViewModel
    {
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public string TypeFilter { get; set; }
        public string SearchFilter { get; set; }
        public int TotalPatients { get; set; }
        public int DogsCount { get; set; }
        public int CatsCount { get; set; }
    }
}