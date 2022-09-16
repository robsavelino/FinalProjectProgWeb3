using FinalProjectProgWeb3.Core.Models;

namespace FinalProjectProgWeb3.Core.Interfaces
{
    public interface ICityEventServices
    {
        CityEvent GetCityEvent(long id);
        List<CityEvent> GetCityEvents();
        List<CityEvent> GetCityEvents(string title);
        List<CityEvent> GetCityEvents(string local, DateTime date);
        List<CityEvent> GetCityEvents(decimal minPrice, decimal maxPrice, DateTime date);
        bool InsertEvent (CityEvent cityEvent);
        bool UpdateEvent (long id, CityEvent cityEvent);
        bool UpdateEvent (long id);
        bool DeleteEvent (long id);
    }
}
