using HospitalSystem.Data;
using HospitalSystem.Models;


namespace HospitalSystem.Services.IServices
{
    public interface IRoom
    {
        void CreateRoom(Room room, HospitalContext context);
        List<Room> GetAllRooms(HospitalContext context);

        Room GetRoom(int roomId, HospitalContext context);

        void UpdateRoom(int roomId, string roomNumber, string roomType, HospitalContext context);

        void DeleteRoom(int roomId, HospitalContext context);
    }
}
