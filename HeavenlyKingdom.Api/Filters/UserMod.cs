using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeavenlyKingdom.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserMod : ActionFilterAttribute
    {
        public override int Order => 3;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new JsonResult(new { Message = "Unauthorized. Please log in." })
                {
                    StatusCode = 401
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
