using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeavenlyKingdom.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FatherMod : ActionFilterAttribute
    {
        public override int Order => 2;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isFather = context.HttpContext.Session.GetString("isFather");

            if (isFather != "true")
            {
                context.Result = new JsonResult(new { Message = "Access denied. Fathers only." })
                {
                    StatusCode = 403
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
