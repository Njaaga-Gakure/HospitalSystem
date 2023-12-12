using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Services;
using HospitalSystem.Services.IServices;

namespace HospitalSystem.Controllers
{
    public class RoomsController
    {
        private readonly RoomsService _roomsService;
        public RoomsController()
        {
           _roomsService = new RoomsService();
        }
        public void ViewAllRooms(HospitalContext context)
        {
            var rooms = _roomsService.GetAllRooms(context);
            Console.WriteLine("\nAvailable Rooms");
            Console.WriteLine("--------------\n");
            if (rooms.Count() > 0)
            {
                foreach (var room in rooms)
                {
                    Console.WriteLine($"\t\tRoom Id: {room.RoomID} Room Number: {room.RoomNumber} | Room Type: {room.RoomType}\n");
                }
            }
            else
            {
                Console.WriteLine("\nNo patients at the moment\n");
            }

        }
        public void ViewRoom(HospitalContext context) {
            do
            {
                ViewAllRooms(context);
                Console.WriteLine("\nEnter Id of the room you want to view.");
                string? roomId = Console.ReadLine();
                Room room;


                try
                {
                    room = ValidateRoomId(roomId, context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("Room Details");
                Console.WriteLine("------------\n");
                Console.WriteLine($"\tRoom Number: {room.RoomNumber} | Room Type: {room.RoomType}\n");
                var patients = room.Patients;
                if (patients != null)
                {
                    Console.WriteLine("\t\tPatients in Room");
                    Console.WriteLine("\t\t-------------------\n");
                    foreach (var patient in patients)
                    {
                        Console.WriteLine($"\t\t\tPatient Name: {patient.FirstName} {patient.LastName}");
                        Console.WriteLine($"\t\t\tPatient Email: {patient.Email}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("\n\tNo patients currently in this room\n");
                }
                break;
            }
            while (true);
        }

        public Room? ValidateRoomId(string roomId, HospitalContext context)
        {
            bool isInteger = int.TryParse(roomId, out int number);
            if (isInteger)
            {
                var room = _roomsService.GetRoom(number, context);
                if (room.RoomType == null)
                {
                    throw new Exception($"\t\tThe room id you entered is not valid\n");
                }
                return room;
            }
            throw new Exception("\t\tThe room id you entered is not valid\n");
        }

    }
}
