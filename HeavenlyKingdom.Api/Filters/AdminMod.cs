using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminMod : ActionFilterAttribute
    {
        public AdminMod() { Order = 1; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (role != "2")
            {
                context.Result = new JsonResult(new { Message = "Access denied. Admins only." })
                {
                    StatusCode = 403
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
