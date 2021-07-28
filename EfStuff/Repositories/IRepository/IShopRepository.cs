using System.Collections.Generic;
using IliaskaWebSite.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IShopRepository : IBaseRepository<Product>
    {
        public List<Gender> GetGender(long id);
    }
}