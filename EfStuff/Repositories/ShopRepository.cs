using System.Linq;
using IliaskaWebSite.EfStuff;
using IliaskaWebSite.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ShopRepository : BaseRepository<Product>, IShopRepository
    {
        public ShopRepository(IliaskaDbContext iliaskaDbContext) : base(iliaskaDbContext)
        {
            
        }
    }
    
}