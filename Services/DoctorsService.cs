using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Services.IServices;


namespace HospitalSystem.Services
{
    public class DoctorsService : IDoctor
    {
        public void CreateDoctor(Doctor doctor, HospitalContext context)
        {
            try
            { 
                context.Doctors.Add(doctor);
                context.SaveChanges();
                Console.WriteLine("Doctor Added Successfully :)");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Doctor> GetAllDoctors(HospitalContext context)
        {
            try
            {
                var doctors = context.Doctors;

                if (doctors != null) 
                {
                    return doctors.ToList();
                }

                return new List<Doctor>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Doctor>();
            }
        }

        public Doctor GetDoctor(int doctorId, HospitalContext context)
        {
            try
            {
                var doctor = context.Doctors
                                .Where(doctor => doctor.DoctorID == doctorId)
                                .FirstOrDefault();
                if (doctor != null)
                {
                    return doctor;
                }
                return new Doctor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Doctor();    
            }
        }

        public void UpdateDoctor(int doctorId, string doctorName, string doctorSpecialty, HospitalContext context)
        {
            try
            {
                var doctor = context.Doctors
                                .Where(doctor => doctor.DoctorID == doctorId)
                                .FirstOrDefault();
                if (doctor != null)
                {
                    doctor.DoctorName = doctorName;
                    doctor.Specialty = doctorSpecialty; 
                    context.SaveChanges();
                    Console.WriteLine("Doctor Updated Successfully :)");
                }
                Console.WriteLine("The doctor you are try to update does not exist :(");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               
            }
        }
        public void DeleteDoctor(int doctorId, HospitalContext context)
        {
            try
            {
                var doctor = context.Doctors
                                .Where(doctor => doctor.DoctorID == doctorId)
                                .FirstOrDefault();
                if (doctor != null)
                {
                    context.Doctors.Remove(doctor);
                    context.SaveChanges();
                    Console.WriteLine("Doctor deleted Successfully :)");
                }
                Console.WriteLine("The doctor you are try to delete does not exist :(");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }

    }
}
