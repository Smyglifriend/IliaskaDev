using System;
using System.ComponentModel.DataAnnotations;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        
        public JobType JobType { get; set; }

    }
}