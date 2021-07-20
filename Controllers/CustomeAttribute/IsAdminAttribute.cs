using IliaskaWebSite.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IliaskaWebSite.Controllers.CustomeAttribute
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userService = (UserService) context
                .HttpContext
                .RequestServices
                .GetService(typeof(UserService));

            if (userService != null && !userService.IsAdmin() )
            {
                context.Result = new ForbidResult();
            }
            base.OnActionExecuted(context);
        }
    }
}