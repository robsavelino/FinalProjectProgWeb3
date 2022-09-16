using Dapper;
using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinalProjectProgWeb3.Infra.Data.Repositories
{
    public class CityEventRepositoy : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        public CityEventRepositoy(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool DeleteEvent(long idEvent)
        {
            var query = "DELETE from CityEvent WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

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
        public CityEvent GetCityEvent(long idEvent)
        {
            var query = "SELECT * from CityEvent WHERE idEvent = @idEvent";
            DynamicParameters parameters = new(new {idEvent});
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }

        }
        public List<CityEvent> GetCityEvents()
        {
            var query = "SELECT * from CityEvent";
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }
        public List<CityEvent> GetCityEvents(string title)
        {
            var query = "SELECT * from CityEvent WHERE title LIKE ('%' + @title + '%')";
            DynamicParameters parameters = new(new { title });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }
        public List<CityEvent> GetCityEvents(string local, DateTime date)
        {
            var query = "SELECT * from CityEvent WHERE local = @local AND CAST(dateHourEvent as DATE) = CAST(@date as DATE)";
            DynamicParameters parameters = new(new { local, date });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }
        public List<CityEvent> GetCityEvents(decimal minPrice, decimal maxPrice, DateTime date)
        {
            var query = "SELECT * from CityEvent WHERE price >= @minPrice AND price <= @maxPrice AND CAST(dateHourEvent as DATE) = CAST(@date as DATE)";
            DynamicParameters parameters = new(new { minPrice, maxPrice, date });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }
        public bool InsertEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";
            var parameters = new DynamicParameters(cityEvent);
   
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
        public bool UpdateEvent(long idEvent,CityEvent cityEvent)
        {
            var query = "UPDATE CityEvent set title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, address = @address, price = @price, status = @status WHERE idEvent = @idEvent";
            DynamicParameters parameters = new (new {cityEvent.Title, cityEvent.Description, cityEvent.DateHourEvent, cityEvent.Local, cityEvent.Address, cityEvent.Price, cityEvent.Status, idEvent});

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
        public bool UpdateEvent(long idEvent)
        {
            var query = "UPDATE CityEvent set status = 0 WHERE idEvent = @idEvent";
            DynamicParameters parameters = new(new { idEvent });

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
