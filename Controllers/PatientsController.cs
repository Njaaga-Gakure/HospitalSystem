using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Services;
using Microsoft.EntityFrameworkCore;


namespace HospitalSystem.Controllers
{
    public class PatientsController
    {
        private readonly PatientServices _patientServices;

        public PatientsController()
        {
            _patientServices = new PatientServices();   
        }
        public void AddPatient(HospitalContext context) {
            do {
                Console.WriteLine("Add a Patient");
                Console.WriteLine("--------------\n");
                Console.WriteLine("Available Rooms\n");
                var rooms = context.Rooms;

                foreach (var room in rooms) {
                    Console.WriteLine($"Room ID: {room.RoomID} | Room Number: {room.RoomNumber} | Room Type: {room.RoomType}");
                }
  
                Console.WriteLine("\nTo add a patient enter first name, last name, email and room id\n");
                Console.WriteLine("Enter Patient's first name.");
                string? firstName = Console.ReadLine();
                Console.WriteLine("Enter Patient's last name.");
                string? lastName = Console.ReadLine();
                Console.WriteLine("Enter Patient's room id.");
                string? roomId = Console.ReadLine();
                Console.WriteLine("Enter Patient's e-mail.");
                string? email = Console.ReadLine();
                var options = new List<string>() { firstName, lastName, roomId, email };
                Room roomToAssign = new();
                try 
                {
                    ValidateOptions(options);
                    roomToAssign = ValidateRoomId(roomId, context);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }
                var patient = new Patient() { FirstName = firstName, LastName=lastName, Email=email, Room = roomToAssign};
                _patientServices.CreatePatient(patient, context);
                break;
            } while (true); 
        }

        public void ViewAllPatients(HospitalContext context) {
            var patients = _patientServices.GetAllPatients(context);
            if (patients == null || patients.Count() == 0)
            {
                Console.WriteLine("\n\t\tNo patients at the moment\n");
                return;
            }
            Console.WriteLine("\nAll Patients");
            Console.WriteLine("----------\n");
            foreach (var patient in patients) {
                Console.WriteLine($"\tPatient Id: {patient.PatientID}");
                Console.WriteLine($"\tName: {patient.FirstName} {patient.LastName}");
                Console.WriteLine($"\tEmail: {patient.Email}");
                Console.WriteLine($"\tRoom Number: {patient.Room.RoomNumber} | Room Type: {patient.Room.RoomType}\n");
            }
        }

        public void ViewSinglePatient(HospitalContext context) {
            do
            {
                    ViewAllPatients (context);  
                    Console.WriteLine("\nEnter id of the patient you want to view: ");
                    string? patientId = Console.ReadLine();

                    int PatientId;

                    try
                    {
                        PatientId = ValidatePatientId(patientId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    var patient = _patientServices.GetPatient(PatientId, context);
                if (patient != null)
                {
                    Console.WriteLine($"\tPatient Id: {patient.PatientID}");
                    Console.WriteLine($"\tName: {patient.FirstName} {patient.LastName}");
                    Console.WriteLine($"\tEmail: {patient.Email}");
                    Console.WriteLine($"\tRoom Number: {patient.Room.RoomNumber} | Room Type: {patient.Room.RoomType}");

                    var appointments = patient.Appointments;
                    var doctors = context.Doctors;

                    if (appointments != null && appointments.Count() != 0)
                    {
                        Console.WriteLine($"\n\t{patient.LastName}'s Appointments");
                        Console.WriteLine("\t--------------------------------------\n");
                        foreach (var appointment in appointments)
                        {
                            var doctor = doctors.Where(doctor => doctor.DoctorID == appointment.DoctorID).FirstOrDefault();
                            Console.WriteLine($"\t\tAppointment by Dr. {doctor?.DoctorName} {doctor?.Specialty} specialist:\n");
                            Console.WriteLine($"\t\t\tAppointment Date: {appointment.AppointmentDate.ToShortDateString()}");
                            Console.WriteLine($"\t\t\tAppointment Time: {appointment.AppointmentTime.ToShortTimeString()}\n");

                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"\n\t{patient.FirstName} currently has no appointments\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n\tPatient does not exists\n");
                }
                    break;   
               
            }
            while (true);
        }

        public void CreateAppointment(HospitalContext context) {
            do
            {
                Console.WriteLine("\nCreate Appointment");
                Console.WriteLine("------------------\n");
                var doctors = context.Doctors;
                var patients = _patientServices.GetAllPatients(context);


                if (patients != null && patients.Count() != 0)
                {
                    Console.WriteLine("\tAvailable Doctors");
                    Console.WriteLine("\t-----------------\n");
                    foreach (var doctor in doctors)
                    {
                        Console.WriteLine($"\t\tDoctor's Id: {doctor.DoctorID} | Doctor's Name: {doctor.DoctorName} | Doctor's Specialty: {doctor.Specialty}");
                    }
                    Console.WriteLine("\n\tAvailable Patients");
                    Console.WriteLine("\t-----------------\n");

                    foreach (var patient in patients)
                    {
                        Console.WriteLine($"\t\tPatient Id: {patient.PatientID}");
                        Console.WriteLine($"\t\tName: {patient.FirstName} {patient.LastName}");
                        Console.WriteLine($"\t\tEmail: {patient.Email}");
                        Console.WriteLine($"\t\tRoom Number: {patient.Room.RoomNumber} | Room Type: {patient.Room.RoomType}\n");
                    }

                    Console.WriteLine("\nEnter id of the patient to set appointment: ");
                    string? patientId = Console.ReadLine();
                    Console.WriteLine("Enter id of the doctor to set appointment with: ");
                    string? doctorId = Console.ReadLine();
                    Doctor appointmentDoctor;
                    int appointmentPatientId;

                    try
                    {
                        appointmentDoctor = ValidateDoctorId(doctorId, doctors);
                        appointmentPatientId = ValidatePatientId(patientId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    var appointment = new Appointment()
                    {
                        AppointmentDate = DateTime.Today,
                        AppointmentTime = DateTime.Now,
                        Doctor = appointmentDoctor
                    };

                    _patientServices.AddPatientAppointment(appointmentPatientId, appointment, context);
                    break;
                }
                else
                {
                    Console.WriteLine("\t\tNo patients currently available\n");
                    break;
                }
            } 
            while (true);
        }

        public void DeletePatient(HospitalContext context)
        {

            do
            {
                Console.WriteLine("\nDelete Patient");
                Console.WriteLine("------------------\n");

                var patients = _patientServices.GetAllPatients(context);

                if (patients != null && patients.Count() != 0)
                {
                    Console.WriteLine("\n\tAvailable Patients");
                    Console.WriteLine("\t-----------------\n");

                    foreach (var patient in patients)
                    {
                        Console.WriteLine($"\t\tPatient Id: {patient.PatientID}");
                        Console.WriteLine($"\t\tName: {patient.FirstName} {patient.LastName}");
                        Console.WriteLine($"\t\tEmail: {patient.Email}");
                        Console.WriteLine($"\t\tRoom Number: {patient.Room.RoomNumber} | Room Type: {patient.Room.RoomType}\n");
                    }
                    Console.WriteLine("\nEnter id of the patient to delete: ");
                    string? patientId = Console.ReadLine();

                    int deletePatientId;

                    try
                    {
                        deletePatientId = ValidatePatientId(patientId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    _patientServices.DeletePatient(deletePatientId, context);
                    break;
                }
                else
                {
                    Console.WriteLine("\t\tNo patients to delete\n");
                    break;
                }
            }
            while (true);


        }

        public void ValidateOptions(List<string> options)
        {
            foreach (var option in options) { 
                if (string.IsNullOrWhiteSpace(option))
                {
                    throw new Exception("\t\tPlease fill in all the fields.\n");
                }
            }
        }
        public Room? ValidateRoomId(string roomId, HospitalContext context) {
            bool isInteger = int.TryParse(roomId, out int number);
            if (isInteger)
            {
                var room = context.Rooms.Where(room => room.RoomID == number).FirstOrDefault();
                if (room == null)
                {    
                    throw new Exception($"\t\tThe room id you entered is not valid\n");
                }
                return room;
            }
            throw new Exception("\t\tThe room id you entered is not valid\n");
        }
        public Doctor? ValidateDoctorId(string id, DbSet<Doctor> doctors)
        {
            bool isInteger = int.TryParse(id, out int number);
            if (isInteger)
            {
                var doctor = doctors.Where(doctor => doctor.DoctorID == number).FirstOrDefault();
                if (doctor == null)
                {
                    throw new Exception($"\t\tThe doctor id you entered is not valid\n");
                }
                return doctor;
            }
            throw new Exception("\t\tThe doctor id you entered is not valid\n");
        }

        public int ValidatePatientId(string patientId) 
        {
            bool isInteger = int.TryParse(patientId, out int number);
            if (isInteger)
            {
                return number;
            }
            throw new Exception("\t\tThe patient id you entered is not valid\n");
        }
    }
}
