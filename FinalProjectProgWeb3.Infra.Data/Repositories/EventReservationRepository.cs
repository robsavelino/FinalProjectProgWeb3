using Dapper;
using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinalProjectProgWeb3.Infra.Data.Repositories
{
    public class EventReservationRepository : IEventReservationRepository
    {

        private readonly IConfiguration _configuration;
        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool DeleteReservation(long idReservation)
        {
            var query = "DELETE from EventReservation WHERE idReservation = @idReservation";
            DynamicParameters parameters = new(new { idReservation });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return false;
            }
        }
        public bool EventHasReservation(long idEvent)
        {
            var query = "SELECT * from EventReservation WHERE idEvent = @idEvent";
            DynamicParameters parameters = new(new { idEvent });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                if (conn.Query<CityEvent>(query, parameters).Count() == 0)
                    return false;
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return false;
            }
        }
        public List<Object> GetReservationFromPerson(string personName, string title)
        {
            var query = "SELECT * FROM CityEvent AS Event INNER JOIN EventReservation AS Reservation ON Event.title LIKE ('%' + @title + '%') AND Reservation.personName = @personName AND Event.idEvent = Reservation.idEvent; ";
            DynamicParameters parameters = new(new { personName, title });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<Object>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }
        public bool InsertReservation(EventReservation reservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity)";
            DynamicParameters parameters = new (new {reservation.IdEvent, reservation.PersonName, reservation.Quantity});

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return false;
            }
        }
        public bool ReservationExists(long idReservation)
        {
            var query = "SELECT * from EventReservation WHERE idReservation = @idReservation";
            DynamicParameters parameters = new(new { idReservation });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                if (conn.Query<CityEvent>(query, parameters).ToList() == null)
                    return false;
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return false;
            }
        }
        public bool UpdateReservation(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation set quantity = @quantity WHERE idReservation = @idReservation";
            DynamicParameters parameters = new(new {quantity, idReservation});

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return false;
            }
        }
    }
}
