using System.Security.Claims;
using IliaskaWebSite.EfStuff.Model;

namespace IliaskaWebSite.Service
{
    public interface IUserService 
    {
        User GetCurrent();
        ClaimsPrincipal GetPrincipal(User user);

        public bool IsAdmin();
    }
}