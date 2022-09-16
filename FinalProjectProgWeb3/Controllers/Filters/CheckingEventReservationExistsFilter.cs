using FinalProjectProgWeb3.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProjectProgWeb3.Controllers.Filters
{
    public class CheckingEventReservationExistsFilter : ActionFilterAttribute
    {
        private readonly IEventReservationServices _eventReservationServices;
        public CheckingEventReservationExistsFilter(IEventReservationServices eventReservationServices)
        {
            _eventReservationServices = eventReservationServices;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idReservation = (long)context.ActionArguments["idReservation"];

            if (!_eventReservationServices.ReservationExists(idReservation))
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);

        }
    }
}
