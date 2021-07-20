using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IliaskaWebSite.EfStuff.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.Models
{
    public class AddProductViewModel
    {
        public AddProductViewModel(List<ProductCategory> categoryNames)
        {
            CategoryOptions = categoryNames.Select(option => new SelectListItem()
            {
                Text = option.Category,
                Value = option.Category
            }).ToList();
            
        }

        public AddProductViewModel()
        {
            
        }

        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "be sure to show the path.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Be sure to enter the name of the product.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "All products have a price.")]
        public double Price { get; set; }
        
        [Required(ErrorMessage = "The amount of products may even be 0.")]
        public int AmountOnRepository { get; set; }

        public string CategoryId { get; set; }
    }
}