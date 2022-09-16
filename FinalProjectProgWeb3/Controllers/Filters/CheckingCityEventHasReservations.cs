using FinalProjectProgWeb3.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProjectProgWeb3.Controllers.Filters
{
    public class CheckingCityEventHasReservations : ActionFilterAttribute
    {
        private readonly IEventReservationServices _eventReservationServices;
        public CheckingCityEventHasReservations(IEventReservationServices eventReservationServices)
        {
            _eventReservationServices = eventReservationServices;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];

            if (!_eventReservationServices.EventHasReservation(idEvent))
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
           
        }
    }
}
