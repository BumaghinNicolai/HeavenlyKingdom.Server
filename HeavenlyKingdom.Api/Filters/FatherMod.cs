using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeavenlyKingdom.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FatherMod : ActionFilterAttribute
    {
        public FatherMod() { Order = 2; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("role");

            if (role != "1" && role != "2")
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
