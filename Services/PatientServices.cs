using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services
{
    public class PatientServices : IPatient
    {
        public void CreatePatient(Patient patient, HospitalContext context)
        {
            try { 
                context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine("\nPatient Added Successfull... :)\n");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        public List<Patient>? GetAllPatients(HospitalContext context)
        {
            try {
                var patients = context.Patients
                                .Include(patient => patient.Appointments)
                                .Include(patient => patient.Room)
                                .ToList();
                return patients ?? null;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Patient? GetPatient(int patientId, HospitalContext context)
        {
            try
            {
                var patient = context.Patients
                                .Where(patient => patient.PatientID == patientId)
                                .Include(patient => patient.Appointments)
                                .Include(patient => patient.Room)
                                .FirstOrDefault();

                return patient ?? null;
            }
            catch (Exception e)
            {  
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public void AddPatientAppointment(int patientId, Appointment appointment, HospitalContext context)
        {
            try
            {
                var patient = context.Patients
                            .Where(patient => patient.PatientID == patientId)
                            .Include(patient => patient.Appointments)
                            .FirstOrDefault();

                if (patient != null)
                {
                    if (patient.Appointments == null)
                    {
                        patient.Appointments = new List<Appointment>();
                    }
                    patient.Appointments.Add(appointment);
                    context.SaveChanges();
                    Console.WriteLine("\nAppointment added successfully... :)\n");
                }
                else { 
                    Console.WriteLine("\nPatient you want to create an appointment for does not exist:(\n");
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void DeletePatient(int patientId, HospitalContext context)
        {
            try 
            {
                var patient = context.Patients.Where(patient => patient.PatientID == patientId).FirstOrDefault();

                if (patient != null) {
                    context.Patients.Remove(patient);
                    context.SaveChanges();
                    Console.WriteLine("\nPatient Deleted Successfully... :)\n");
                    return;
                }
                Console.WriteLine("\n Patient you want to delete does not exist :(\n");
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                    
            }
        }

    }
}
