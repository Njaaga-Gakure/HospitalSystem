using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string RoomType { get; set; } = null!;
        
        public List<Patient>? Patients { get; set; } 
    }
}
