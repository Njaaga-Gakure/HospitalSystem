using HospitalSystem;
using HospitalSystem.Controllers;
using HospitalSystem.Data;

using HospitalContext context  = new HospitalContext();

try
{
  var rooms = context.Rooms;
  var doctors = context.Doctors;
    if (rooms.Count() == 0 && doctors.Count() == 0)
    {
        InitializeSystem.IntializeRoomsAndDoctors(context);
    }
    else { 
        HIMSController hIMSController = new HIMSController();
        hIMSController.Index(context);
    }
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}