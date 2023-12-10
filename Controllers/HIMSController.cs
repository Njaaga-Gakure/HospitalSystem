using HospitalSystem.Data;


namespace HospitalSystem.Controllers
{
    public class HIMSController
    {
        private readonly PatientsController _patientsController;

        public HIMSController()
        {
            _patientsController = new PatientsController();
        }
        public void Index(HospitalContext context)
        {
            do {
                Console.WriteLine("Hospital Management System");
                Console.WriteLine("--------------------------\n");
                Console.WriteLine("Press (1) to add a patient.");
                Console.WriteLine("Press (2) to view all patients.");
                Console.WriteLine("Press (3) to view a single patient.");
                Console.WriteLine("Press (4) to add a patient appointment.");
                Console.WriteLine("Press (5) to delete a patient.");
                string? option = Console.ReadLine();
                if (option == "q")
                {
                    Console.WriteLine("Bye...");
                    break;
                }
                var options = new List<string>() { "1", "2", "3", "4", "5"};
                try
                {
                    ValidateOptions(option, options);

                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                RedirectUser(option, context);
            } while (true);
        }

        public void RedirectUser(string option, HospitalContext context) { 
                switch(option) {
                    case "1":
                        _patientsController.AddPatient(context);
                        break;
                    case "2":
                        _patientsController.ViewAllPatients(context);
                        break;
                    case "3":
                        _patientsController.ViewSinglePatient(context);
                        break;
                    case "4":
                        _patientsController.CreateAppointment(context);
                        break;
                    case "5":
                        _patientsController.DeletePatient(context);
                        break;
            }
        }
        public void ValidateOptions(string option, List<string> options) {
            if (string.IsNullOrWhiteSpace(option) || !options.Contains(option)) {
                throw new Exception("\t\tInvalid option.\n");
            }
        }
    }
}
