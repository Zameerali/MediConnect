using System.Collections.Generic;

namespace MediConnectwithAuthentication.Models.Interfaces
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAppointments();
        void AddAppointment(Appointment appointment);
        void DeleteAppointment(int id);
        void UpdateAppointment(Appointment appointment);
        Appointment GetAppointmentById(int id); 
    }
}
