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
            var product = _shopRepository.GetAll();
            var model  = _mapper.Map<List<ProductViewModel>>(product);
            
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProducts(long id = 0)
        {
            //как это оптимизировать, чтобы появились категории
            var category = _categoryRepository
                .GetAll();
            var product = _shopRepository.Get(id);
            var model = _mapper.Map<AddProductViewModel>(product)
                        ?? new AddProductViewModel(category);
            
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
            
            var categoryList = _categoryRepository
                .GetAll();
            var newModelWithCategory = new AddProductViewModel(categoryList);
            
            return View(newModelWithCategory);
        }
        
        public IActionResult Remove(long id)
        {
            _shopRepository.Remove(id);
            return RedirectToAction("Shop");
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
            var product = _mapper.Map<ProductCategories>(model);
            _categoryRepository.Save(product);
            return View(model);
        }

        public JsonResult ShopFilter()
        {
            
            
            return Json(true);
        }
    }
}