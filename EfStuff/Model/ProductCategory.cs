using System;
using System.Collections.Generic;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.EfStuff.Model
{
    public class ProductCategory : BaseModel
    {
        public string Category { get; set; } 
        
        public virtual List<Product> Clothes { get; set; }
    }
}