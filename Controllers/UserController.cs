using System.Threading.Tasks;
using AutoMapper;
using IliaskaWebSite.Controllers.CustomeAttribute;
using IliaskaWebSite.EfStuff.Model;
using IliaskaWebSite.Models;
using IliaskaWebSite.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Repositories;

namespace IliaskaWebSite.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IUserService _userService;
        
        public UserController(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            var model = new RegistrationViewModel();
            var returnUrl = Request.Query["ReturnUrl"];
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            var user = _userRepository.Get(model.Login);
        
            if (user == null || user.Password != model.Password)
            {
                return View(model);
            }
        
            await HttpContext.SignInAsync(_userService.GetPrincipal(user));
            
            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Profile", "User");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Home", "Home");
        }
        
        [IsAdmin]
        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }
        
        [IsAdmin]
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            var isUserUniq = _userRepository.Get(model.Login) == null;
            if (isUserUniq)
            {
                var user = _mapper.Map<User>(model);
                _userRepository.Save(user);
                await HttpContext.SignInAsync(_userService.GetPrincipal(user));
                return RedirectToAction();
            }
            return View(model);
        }
        
        //[Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrent();
            var model = _mapper.Map<ProfileViewModel>(user);
            
            if (_userService.Form())
            {
                model.NullField = true;
            }
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (_userService.Form())
            {
                return RedirectToAction("UserForm");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UserForm()
        {
            
            return View();
        }
    }
}