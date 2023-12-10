using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystem.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public Patient Patient { get; set; } = null!;

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public Doctor Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        
    }
}
