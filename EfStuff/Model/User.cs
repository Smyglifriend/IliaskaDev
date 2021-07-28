using System;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        
        public string SurName { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Email { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }

        public int Age { get; set; }
        
        public JobType JobType { get; set; }
        
        public string AvatarUrl { get; set; }
    }
}