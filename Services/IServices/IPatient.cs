using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem.Services.IServices
{
    public interface IPatient
    {
        void CreatePatient(Patient patient, HospitalContext context);
        List<Patient>? GetAllPatients(HospitalContext context); 

        Patient? GetPatient(int patientId, HospitalContext context);

        void AddPatientAppointment(int patientId, Appointment appointment, HospitalContext context);

        void DeletePatient(int patientId, HospitalContext context);
    }
}
