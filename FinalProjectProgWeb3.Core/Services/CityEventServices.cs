using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;

namespace FinalProjectProgWeb3.Core.Services
{
    public class CityEventServices : ICityEventServices
    {
        public ICityEventRepository _cityEventRepository { get; set ; }
        public IEventReservationServices _eventReservationServices{ get; set; }
        public CityEventServices(ICityEventRepository cityEventRepository, IEventReservationServices eventReservationServices)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationServices = eventReservationServices;
        }

        public bool DeleteEvent(long idEvent)
        {
            if (_eventReservationServices.EventHasReservation(idEvent))
                return _cityEventRepository.UpdateEvent(idEvent);

            return _cityEventRepository.DeleteEvent(idEvent);
        }
        public List<CityEvent> GetCityEvents()
        {
            return _cityEventRepository.GetCityEvents();
        }
        public List<CityEvent> GetCityEvents(string title)
        {
            return _cityEventRepository.GetCityEvents(title);
        }
        public List<CityEvent> GetCityEvents(string local, DateTime date)
        {
            return _cityEventRepository.GetCityEvents(local, date);
        }
        public List<CityEvent> GetCityEvents(decimal minPrice, decimal maxPrice, DateTime date)
        {
            return _cityEventRepository.GetCityEvents(minPrice, maxPrice, date);
        }
        public bool InsertEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertEvent(cityEvent);
        }
        public bool UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateEvent(idEvent, cityEvent);
        }
        public CityEvent GetCityEvent(long id)
        {
            return _cityEventRepository.GetCityEvent(id);
        }
        public bool UpdateEvent(long id)
        {
            return _cityEventRepository.UpdateEvent(id);
        }
    }
}
