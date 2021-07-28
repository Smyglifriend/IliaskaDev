using System;
using IliaskaWebSite.EfStuff.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Repositories;

namespace IliaskaWebSite.EfStuff
{
    public static class SeedExtansion
    {
        public const string AdminName = "Admin";
        public static IHost SeedData(this IHost server)
        {
            using (var serverScope = server.Services.CreateScope())
            {
                SetDefaultUser(serverScope.ServiceProvider);
            }

            return server;
        }

        private static void SetDefaultUser(IServiceProvider server)
        {
            var userRepository = server.GetService<UserRepository>();

            var admin = userRepository.Get("Admin");
            if (admin == null)
            {
                admin = new User()
                {
                    Name = AdminName,
                    SurName = AdminName,
                    Login = AdminName
                };
                
                userRepository.Save(admin);
            }
        }
    }
}