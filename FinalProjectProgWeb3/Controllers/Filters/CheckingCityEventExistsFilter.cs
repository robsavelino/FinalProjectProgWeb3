using FinalProjectProgWeb3.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProjectProgWeb3.Controllers.Filters
{
    public class CheckingCityEventExistsFilter : ActionFilterAttribute
    {
        public ICityEventServices _cityEventServices;
        public CheckingCityEventExistsFilter (ICityEventServices cityEventServices)
        {
            _cityEventServices = cityEventServices;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long id = (long)context.ActionArguments["idEvent"];

            if (_cityEventServices.GetCityEvent(id) == null)
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
        }


    }
}
