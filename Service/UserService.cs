using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using IliaskaWebSite.EfStuff.Model;
using IliaskaWebSite.Models;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;

namespace IliaskaWebSite.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public User GetCurrent()
        {
            var http = _contextAccessor.HttpContext;
            
            var idStr = http.User
                    ?.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            
                if (string.IsNullOrEmpty(idStr))
                {
                    return null;
                }   

                var id = long.Parse(idStr);
                return _userRepository.Get(id);
        }
        
        public ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return principal;
        }

        public bool IsAdmin()
        {
            var user = GetCurrent();
            return user.JobType == JobType.Admin;
        }
        
        public bool isOfficeWorker()
        {
            var user = GetCurrent();
            return user.JobType == JobType.OfficeWorker;
        }

        public bool isHR()
        {
            var user = GetCurrent();
            return user.JobType == JobType.HR;
        }

        public bool Form()
        {
            var user = GetCurrent();
            var model = _mapper.Map<ProfileViewModel>(user);
            if (model.Age != 0 &&
                model.Email != null &&
                model.Name != null &&
                model.SurName != null)
            {
                return true;
            }
        
            return false;
        }
    }
}