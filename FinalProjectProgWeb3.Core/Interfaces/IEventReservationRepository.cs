using FinalProjectProgWeb3.Core.Models;

namespace FinalProjectProgWeb3.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<Object> GetReservationFromPerson(string personName, string title);
        bool DeleteReservation(long idReservation);
        bool UpdateReservation(long idReservation, long quantity);
        bool InsertReservation(EventReservation reservation);
        bool EventHasReservation(long idEvent);
        bool ReservationExists (long idReservation);

    }
}
