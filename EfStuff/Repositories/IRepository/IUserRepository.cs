using IliaskaWebSite.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Get(string data);
    }
}