using Microsoft.Data.SqlClient;
using MediConnectwithAuthentication.Models;
using MediConnectwithAuthentication.Models.Interfaces;

namespace MediConnectwithAuthentication.Models.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MediConnect;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private readonly DBContext dbContext = new DBContext();
        public List<Appointment> GetAppointments()
        {
            Console.WriteLine("Reached in GEt");
            //List<Appointment> appointments = new List<Appointment>();

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = "SELECT Id, DoctorName, AppointmentDate, TimeSlot, Reason, Status FROM Appointments";
            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                appointments.Add(new Appointment
            //                {
            //                    Id = reader.GetInt32(0),
            //                    DoctorName = reader.GetString(1),
            //                    AppointmentDate = reader.GetDateTime(2),
            //                    TimeSlot = reader.GetString(3),
            //                    Reason = reader.GetString(4),
            //                    Status = reader.GetString(5)
            //                });
            //            }
            //        }
            //    }
            //}
            try
            {
                var appointments = dbContext.Appointments.ToList();

                return appointments;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving appointments: " + ex.Message);
                return new List<Appointment>();
            }

        }

        public void AddAppointment(Appointment a)
        {
            Console.WriteLine("Reached in ADd");
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        string query = @"INSERT INTO Appointments (DoctorName, AppointmentDate, TimeSlot, Reason, Status) 
            //                 VALUES (@DoctorName, @AppointmentDate, @TimeSlot, @Reason, @Status)";

            //        using (SqlCommand cmd = new SqlCommand(query, conn))
            //        {
            //            cmd.Parameters.AddWithValue("@DoctorName", a.DoctorName);
            //            cmd.Parameters.AddWithValue("@AppointmentDate", a.AppointmentDate);
            //            cmd.Parameters.AddWithValue("@TimeSlot", a.TimeSlot);
            //            cmd.Parameters.AddWithValue("@Reason", a.Reason);
            //            cmd.Parameters.AddWithValue("@Status", a.Status ?? "Pending");

            //            int rowsAffected = cmd.ExecuteNonQuery();
            //            Console.WriteLine($"Rows inserted: {rowsAffected}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error inserting appointment: " + ex.Message);
            //}
            try
            {
                dbContext.Appointments.Add(a);

                dbContext.SaveChanges();
                Console.WriteLine("Appointment added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting appointment: " + ex.Message);
            }
        }
        public Appointment GetAppointmentById(int id)
        {
            //Appointment appointment = null;
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = "SELECT Id, DoctorName, AppointmentDate, TimeSlot, Reason, Status FROM Appointments WHERE Id = @Id";
            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@Id", id);
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                appointment = new Appointment
            //                {
            //                    Id = reader.GetInt32(0),
            //                    DoctorName = reader.GetString(1),
            //                    AppointmentDate = reader.GetDateTime(2),
            //                    TimeSlot = reader.GetString(3),
            //                    Reason = reader.GetString(4),
            //                    Status = reader.GetString(5)
            //                };
            //            }
            //        }
            //    }
            //}
            //return appointment;
            return dbContext.Appointments.Find(id);
        }
        public void UpdateAppointment(Appointment updatedAppointment)
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        string query = @"UPDATE Appointments SET 
            //                DoctorName = @DoctorName,
            //                AppointmentDate = @AppointmentDate,
            //                TimeSlot = @TimeSlot,
            //                Reason = @Reason,
            //                Status = @Status 
            //                WHERE Id = @Id";

            //        using (SqlCommand cmd = new SqlCommand(query, conn))
            //        {
            //            cmd.Parameters.AddWithValue("@DoctorName", updatedAppointment.DoctorName);
            //            cmd.Parameters.AddWithValue("@AppointmentDate", updatedAppointment.AppointmentDate);
            //            cmd.Parameters.AddWithValue("@TimeSlot", updatedAppointment.TimeSlot);
            //            cmd.Parameters.AddWithValue("@Reason", updatedAppointment.Reason);
            //            cmd.Parameters.AddWithValue("@Status", updatedAppointment.Status);
            //            cmd.Parameters.AddWithValue("@Id", updatedAppointment.Id);

            //            int rowsAffected = cmd.ExecuteNonQuery();
            //            Console.WriteLine($"Rows updated: {rowsAffected}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error updating appointment: " + ex.Message);
            //}
            try
            {
                Appointment appointment = dbContext.Appointments.Find(updatedAppointment.Id);

                if (appointment != null)
                {
                    appointment.DoctorName = updatedAppointment.DoctorName;
                    appointment.AppointmentDate = updatedAppointment.AppointmentDate;
                    appointment.TimeSlot = updatedAppointment.TimeSlot;
                    appointment.Reason = updatedAppointment.Reason;
                    appointment.Status = updatedAppointment.Status;

                    dbContext.SaveChanges();                }
                else
                {
                    Console.WriteLine("Appointment not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating appointment: " + ex.Message);
            }

        }
        public void DeleteAppointment(int id)
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        string query = "DELETE FROM Appointments WHERE Id = @Id";

            //        using (SqlCommand cmd = new SqlCommand(query, conn))
            //        {
            //            cmd.Parameters.AddWithValue("@Id", id);
            //            int rowsAffected = cmd.ExecuteNonQuery();
            //            Console.WriteLine($"Rows deleted: {rowsAffected}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error deleting appointment: " + ex.Message);
            //}
        {
            try
            {
                Appointment appointment = dbContext.Appointments.Find(id);

                if (appointment != null)
                {
                    dbContext.Appointments.Remove(appointment);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Appointment not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting appointment: " + ex.Message);
            }
        }

    }


}
}
