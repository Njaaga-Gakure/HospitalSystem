using HospitalSystem.Data;
using HospitalSystem.Models;


namespace HospitalSystem.Services.IServices
{
    public interface IDoctor
    {
        void CreateDoctor(Doctor doctor, HospitalContext context);
        List<Doctor> GetAllDoctors(HospitalContext context);

        Doctor GetDoctor(int doctorId, HospitalContext context);

        void UpdateDoctor(int doctorId, string doctorName, string doctorSpecialty, HospitalContext context);

        void DeleteDoctor(int doctorId, HospitalContext context);
    }
}
