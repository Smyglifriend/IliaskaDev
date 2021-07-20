using System.Collections.Generic;
using IliaskaWebSite.EfStuff.Model;
using IliaskaWebSite.Migrations;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface ICategoryRepository: IBaseRepository<ProductCategory>
    {
        public ProductCategory Get(string data);
        
    }
}