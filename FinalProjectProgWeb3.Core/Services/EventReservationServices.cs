using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;

namespace FinalProjectProgWeb3.Core.Services
{
    public class EventReservationServices : IEventReservationServices
    {
        private IEventReservationRepository _eventReservationRepository;
        public EventReservationServices(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }   

        public bool DeleteReservation(long idReservation)
        {
            return _eventReservationRepository.DeleteReservation(idReservation);
        }

        public bool EventHasReservation(long idEvent)
        {
            return _eventReservationRepository.EventHasReservation(idEvent);
        }

        public List<Object> GetReservationsFromPerson(string personName, string title)
        {
            return _eventReservationRepository.GetReservationFromPerson(personName, title);
        }

        public bool InsertReservation(EventReservation reservation)
        {
            return _eventReservationRepository.InsertReservation(reservation);
        }

        public bool ReservationExists(long idReservation)
        {
            return _eventReservationRepository.ReservationExists(idReservation);
        }

        public bool UpdateReservation(long idReservation, long quantity)
        {
            return _eventReservationRepository.UpdateReservation(idReservation, quantity);
        }
    }
}
