using FinalProjectProgWeb3.Controllers.Filters;
using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectProgWeb3.Controllers
{
    [ApiController]
    [Route("[controller]/CityEvents")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class CityEventsController : ControllerBase
    {
        public ICityEventServices _cityEventServices;
        public IEventReservationServices _eventReservationServices;
        public CityEventsController(ICityEventServices cityEventServices, IEventReservationServices eventReservationServices)
        {
            _cityEventServices = cityEventServices;
            _eventReservationServices = eventReservationServices;
        }

        #region GETS
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CityEvent> GetCityEvents()
        {
            var cityEvents = _cityEventServices.GetCityEvents();
            if (cityEvents == null)
                return NotFound();
            return Ok(cityEvents);
        }
        [HttpGet("Title")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetCityEvents([FromQuery]string title)
        {
            var cityEvents = _cityEventServices.GetCityEvents(title);
            if (cityEvents == null)
                return NoContent();
            return Ok(cityEvents);
        }
        [HttpGet("Local&Date")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <List<CityEvent>> GetCityEvents([FromQuery]string local, [FromQuery]DateTime date)
        {
            var cityEvents = _cityEventServices.GetCityEvents(local, date);
            if (cityEvents == null)
                return NoContent();
            return Ok(cityEvents);
        }
        [HttpGet("Price&Date")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<CityEvent> GetCityEvents([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice, [FromQuery] DateTime date)
        {
            var cityEvents = _cityEventServices.GetCityEvents(minPrice, maxPrice, date);
            if (cityEvents == null)
                return NoContent();
            return Ok(cityEvents);
        }
        #endregion

        #region POST
        [HttpPost("Insert")]
        [Authorize("admin")]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEvent> InsertEvent(CityEvent newCityEvent)
        {
            var cityEvent = _cityEventServices.InsertEvent(newCityEvent);
            if (!cityEvent)
                return BadRequest();
            return CreatedAtAction(nameof(InsertEvent), newCityEvent);
        }
        #endregion
        #region PUT
        [HttpPut("Update/{idEvent}")]
        [Authorize("admin")]
        [ServiceFilter(typeof(CheckingCityEventExistsFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            var updatedEvent = _cityEventServices.UpdateEvent(idEvent, cityEvent);
            if (!updatedEvent)
                return BadRequest();
            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("Delete/{idEvent}")]
        [Authorize("admin")]
        [ServiceFilter(typeof(CheckingCityEventExistsFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEvent(long idEvent)
        {
            return Ok(_cityEventServices.DeleteEvent(idEvent));
        }
        #endregion
    }
}