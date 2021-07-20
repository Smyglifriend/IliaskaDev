using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IliaskaWebSite.Models;

namespace IliaskaWebSite.Controllers
{
    public class WorkPageController : Controller
    {
        private readonly ILogger<WorkPageController> _logger;

        public WorkPageController(ILogger<WorkPageController> logger)
        {
            _logger = logger;
        }

        public IActionResult WorkPage()
        {
            return View();
        } 
    }
}