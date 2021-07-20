using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using IliaskaWebSite.EfStuff;
using IliaskaWebSite.EfStuff.Model;
using IliaskaWebSite.Models;
using IliaskaWebSite.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff.Repositories;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace IliaskaWebSite
{
    public class Startup
    {
        public const string AuthMethod = "FunCookie";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("connectionString");
            services.AddDbContext<IliaskaDbContext>(x => x.UseSqlServer(connectionString));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Iliaska";
                    config.LoginPath = "/User/Login";
                    config.AccessDeniedPath = "/User/AccessDenied";
                });

            services.AddScoped<IUserService>(diContainer =>
                new UserService(
                    diContainer.GetService<IUserRepository>(),
                    diContainer.GetService<IHttpContextAccessor>()
                    ));
            
            
            
            RegisterMapper(services);
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            RegistrationRepositories(services);
        }
        
        private void RegistrationRepositories(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
        
            var types = assembly.GetTypes();
        
            foreach (var iRepo in types.Where(type =>
                type.IsInterface
                && type.GetInterfaces()
                    .Any(x =>
                        x.IsGenericType
                        && x.GetGenericTypeDefinition() == typeof(IBaseRepository<>))
            ))
            {
                var realization = types.Single(x => x.GetInterfaces().Contains(iRepo));
                services.AddScoped(
                    iRepo,
                    diContainer =>
                    {
                        var constructor = realization.GetConstructors()[0];
                        var paramInfoes = constructor.GetParameters();
        
                        var paramValues = new object[paramInfoes.Length];
                        for (int i = 0; i < paramInfoes.Length; i++)
                        {
                            var paramInfo = paramInfoes[i];
                            var paramValue = diContainer.GetService(paramInfo.ParameterType);
                            paramValues[i] = paramValue;
                        }
        
                        var answer = constructor.Invoke(paramValues);
                        return answer;
                    });
            }
        }
        
        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();
            
            MapBoth<User, UserViewModel>(configExpression);
            MapBoth<User, RegistrationViewModel>(configExpression);
            MapBoth<Product, ProductViewModel>(configExpression);
            MapBoth<Product, AddProductViewModel>(configExpression);
            MapBoth<ProductCategory, CategoriesViewModel>(configExpression);

            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        public void MapBoth<Type1, Type2>(MapperConfigurationExpression configExpression)
        {
            configExpression.CreateMap<Type1, Type2>();
            configExpression.CreateMap<Type2, Type1>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}