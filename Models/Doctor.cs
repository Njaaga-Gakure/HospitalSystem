namespace HospitalSystem.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }

        public string DoctorName { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public List<Appointment>? Appointments { get; set; } = null!; 
    }
}
