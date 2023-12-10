using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem
{
    public class InitializeSystem
    {
        public static void IntializeRoomsAndDoctors(HospitalContext context) {
            try {
                var general = new Room()
                {
                    RoomNumber = "01",
                    RoomType = "General Ward"
                };
                var intensive = new Room()
                {
                    RoomNumber = "01",
                    RoomType = "ICU"
                };
                var generalPractice = new Doctor()
                {
                    DoctorName = "Rebecca",
                    Specialty = "General Practice"
                };

                var specialist = new Doctor()
                {
                    DoctorName = "Samuel",
                    Specialty = "ENT"
                };

                context.Rooms.Add(general);
                context.Rooms.Add(intensive);
                context.Doctors.Add(generalPractice);
                context.Doctors.Add(specialist);
                context.SaveChanges();
                Console.WriteLine("System Initialized successfully..... :)");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
