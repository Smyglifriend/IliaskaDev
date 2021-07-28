using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IliaskaWebSite.EfStuff.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "be sure to show the path.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Be sure to enter the name of the product.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "All products have a price.")]
        public double Price { get; set; }
        
        [Required(ErrorMessage = "The amount of products may even be 0.")]
        public int AmountOnRepository { get; set; }

        public string Category { get; set; }
        
        public Gender Gender { get; set; }
    }
}