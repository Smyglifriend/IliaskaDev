using System.Collections.Generic;
using System.Linq;
using IliaskaWebSite.EfStuff;
using IliaskaWebSite.EfStuff.Model;
using IliaskaWebSite.Models;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ShopRepository : BaseRepository<Product>, IShopRepository
    {
        public ShopRepository(IliaskaDbContext iliaskaDbContext) : base(iliaskaDbContext)
        {
            
        }

        // public List<Product> GetGender()
        // {
        //     return _dbSet
        //         .Select(x => x.Gender)
        //         .Distinct()
        //         .ToList();
        // }
        public List<Gender> GetGender(long productId)
        {
            return _dbSet
                .Where(x =>x.Id == productId)
                .Select(x => x.Gender)
                .Distinct()
                .ToList();
        }
    }
    
}