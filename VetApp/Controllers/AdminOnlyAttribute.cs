using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VetApp.Models;

public class AdminOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var dbContext = context.HttpContext.RequestServices.GetService<PetDbContext>();

        var appUser = dbContext.Users.Find(userId);
        if (appUser == null || !appUser.IsAdmin)
        {
            context.Result = new ForbidResult();
        }
    }
}
