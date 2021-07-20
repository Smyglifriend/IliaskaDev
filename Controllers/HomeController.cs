using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IliaskaWebSite.Controllers.CustomeAttribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IliaskaWebSite.Models;

namespace IliaskaWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}