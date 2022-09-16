using FinalProjectProgWeb3.Controllers.Filters;
using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectProgWeb3.Controllers
{
    [ApiController]
    [Route("[controller]/eventsReservations")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class EventsReservationsController : ControllerBase
    {
        public IEventReservationServices _eventReservationServices;
        public EventsReservationsController(IEventReservationServices eventReservationServices)
        {
            _eventReservationServices = eventReservationServices;
        }

        [HttpGet("GetReservations")]
        [Authorize("admin, cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Object>> GetReservations(string personName, string title)
        {
            var reservations = _eventReservationServices.GetReservationsFromPerson(personName, title);
            if (reservations == null)
                return NoContent();
            return Ok(reservations);
        }

        [HttpPost("InsertReservation")]
        [Authorize("admin, cliente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<EventReservation> InsertReservation(EventReservation reservation)
        {
            var newReservation = _eventReservationServices.InsertReservation(reservation);
            if (!newReservation)
                return BadRequest();
            return Ok(reservation);
        }

        [HttpPut("UpdateReservation")]
        [Authorize("admin")]
        [ServiceFilter(typeof(CheckingEventReservationExistsFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> UpdateReservation([FromQuery]long idReservation, [FromQuery]long quantity)
        {
            var reservation = _eventReservationServices.UpdateReservation(idReservation, quantity);
            if (!reservation)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("DeleteReservation")]
        [Authorize("admin")]
        [ServiceFilter(typeof(CheckingEventReservationExistsFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> DeleteReservation(long idReservation)
        {
            return Ok(_eventReservationServices.DeleteReservation(idReservation));
        }

    }
}
