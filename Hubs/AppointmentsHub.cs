using MediConnectwithAuthentication.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MediConnectwithAuthentication.Hubs
{
    public class AppointmentsHub : Hub
    {
        public async Task SendAppointmentAdded(Appointment appointment)
        {
            await Clients.All.SendAsync("ReceiveAppointment", appointment);
        }
    }
}
