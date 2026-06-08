using HeavenlyKingdom.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FatherMod : ActionFilterAttribute
    {
        public FatherMod() { Order = 2; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var roleValue = int.TryParse(role, out var r) ? r : -1;

            if (roleValue != (int)UserRole.Father && roleValue != (int)UserRole.Admin)
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
