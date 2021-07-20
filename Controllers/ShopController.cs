using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IliaskaWebSite.Controllers.CustomeAttribute;
using IliaskaWebSite.EfStuff.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IliaskaWebSite.Models;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.EfStuff.Repositories;

namespace IliaskaWebSite.Controllers
{
    public class ShopController : Controller
    {
        private IMapper _mapper;
        private IShopRepository _shopRepository;
        private ICategoryRepository _categoryRepository;

        public ShopController(IMapper mapper, IShopRepository shopRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _shopRepository = shopRepository;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public IActionResult Shop()
        {
            var model  = _shopRepository.GetAll().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProducts()
        {
            var category = _categoryRepository
                .GetAll();
            var model = new AddProductViewModel(category);
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddProducts(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            var category = _categoryRepository
                .Get(model.CategoryId);
            
            var product = _mapper.Map<Product>(model);
            product.Category = category;
            _shopRepository.Save(product);
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new CategoriesViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCategory(CategoriesViewModel model)
        {
            var product = _mapper.Map<ProductCategory>(model);
            _categoryRepository.Save(product);
            return View(model);
        }
    }
}