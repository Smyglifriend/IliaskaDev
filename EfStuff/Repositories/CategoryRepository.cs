using System.Collections.Generic;
using System.Linq;
using IliaskaWebSite.EfStuff;
using IliaskaWebSite.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class CategoryRepository : BaseRepository<ProductCategories>, ICategoryRepository
    {
        public CategoryRepository(IliaskaDbContext iliaskaDbContext) : base(iliaskaDbContext)
        {
            
        }
        
        public ProductCategories Get(string data)
        {
            return _dbSet.SingleOrDefault(x => x.Category == data);
        }

        public ProductCategories Get(List<string> modelCategoryId)
        {
            throw new System.NotImplementedException();
        }
    }
    
}