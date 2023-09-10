using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany_2.Domain;
using MyCompany_2.Domain.Repositories.Abstract;
using MyCompany_2.Domain.Repositories.EntityFramework;
using MyCompany_2.Service;


namespace MyCompany_2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем конфиг из appsettings.json
            // Связывам раздел Project с нашим конфиг файлом
            Configuration.Bind("Project", new Config());

            // Подлкючаем нужный функционал приложения в качестве сервисов
            //services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();

            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // Подлкючаем контекст БД. Строка подключения к БД лежит в конфиге.
            services.AddDbContext<AppDBContext>(el => el.UseSqlServer(Config.ConnectionString));

            // Настраиваем identity систему
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

            // Настраиваем authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            // Добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews()
                // выставляем совместимость с asp.net core 3.0
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! порядок регистрации middleware очень важен

            // Вывод ошибок во время разработки
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            // 1. Подлкючение системы маршрутизации
            app.UseRouting();

            // 2. подключаем аутентификацию и авторизацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // 3. Подключаем поддержку статичных файлов в приложении (css, js и т.д.)
            app.UseStaticFiles();

            // Маршрутизация
            // 4. Регистрируем нужные нам маршруты (ендпоинты)
            app.UseEndpoints(endpoints =>
            {
                // загрузка главной страницы, если не заданно другое
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
