using System;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.EfStuff.Model
{
    public class Product : BaseModel
    {
        public string ImageUrl { get; set; }

        public string Name { get; set; }
        
        public double Price { get; set; }
        
        public int AmountOnRepository { get; set; }
        public virtual ProductCategories Category { get; set; }
        
        public Gender Gender { get; set; }
    }
}