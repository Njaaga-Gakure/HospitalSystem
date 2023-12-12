using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services
{
    public class RoomsService : IRoom
    {
        public void CreateRoom(Room room, HospitalContext context)
        {
            try
            {
                context.Rooms.Add(room);
                context.SaveChanges();  
                Console.WriteLine("Room Added Successfully... :)");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public List<Room> GetAllRooms(HospitalContext context)
        {
            try
            { 
                var rooms = context.Rooms;
                if (rooms == null || rooms.Count() == 0) { 
                    return new List<Room>();
                }
                return rooms.ToList();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return new List<Room>();
            }
        }

        public Room GetRoom(int roomId, HospitalContext context)
        {
            try
            {
                var room = context.Rooms
                                .Where(room => room.RoomID == roomId)
                                .Include(room => room.Patients)
                                .FirstOrDefault();

                if (room != null) 
                {
                    return room;
                }
                return new Room();  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Room();
            }
        }

        public void UpdateRoom(int roomId, string roomNumber, string roomType, HospitalContext context)
        {
            try
            {
                var room = context.Rooms.Where(room => room.RoomID == roomId).FirstOrDefault();

                if (room != null)
                {
                    room.RoomNumber = roomNumber;   
                    room.RoomType = roomType;
                    context.SaveChanges();
                    Console.WriteLine("Room updated successfully");
                }
                Console.WriteLine("The room you are trying to update does not exist :(");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteRoom(int roomId, HospitalContext context)
        {
            try
            {
                var room = context.Rooms.Where(room => room.RoomID == roomId).FirstOrDefault();

                if (room != null)
                {
                    context.Rooms.Remove(room); 
                    context.SaveChanges();
                    Console.WriteLine("Room removed successfully");
                }
                Console.WriteLine("The room you are trying to delete does not exist :(");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
