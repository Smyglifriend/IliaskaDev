using System.Collections.Generic;
using IliaskaWebSite.EfStuff.Model;
using ProductCategories = IliaskaWebSite.EfStuff.Model.ProductCategories;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface ICategoryRepository: IBaseRepository<ProductCategories>
    {
        public ProductCategories Get(string data);
    }
}