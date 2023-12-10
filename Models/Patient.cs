using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystem.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        [ForeignKey("Room")]
        public int RoomID { get; set; }

        public Room Room { get; set; } = null!;
        
        public List<Appointment>? Appointments { get; set; } 
    }
}
