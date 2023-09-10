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
            // ���������� ������ �� appsettings.json
            // �������� ������ Project � ����� ������ ������
            Configuration.Bind("Project", new Config());

            // ���������� ������ ���������� ���������� � �������� ��������
            //services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();

            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // ���������� �������� ��. ������ ����������� � �� ����� � �������.
            services.AddDbContext<AppDBContext>(el => el.UseSqlServer(Config.ConnectionString));

            // ����������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

            // ����������� authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            // ��������� ��������� ������������ � ������������� (MVC)
            services.AddControllersWithViews()
                // ���������� ������������� � asp.net core 3.0
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! ������� ����������� middleware ����� �����

            // ����� ������ �� ����� ����������
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            // 1. ����������� ������� �������������
            app.UseRouting();

            // 2. ���������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // 3. ���������� ��������� ��������� ������ � ���������� (css, js � �.�.)
            app.UseStaticFiles();

            // �������������
            // 4. ������������ ������ ��� �������� (���������)
            app.UseEndpoints(endpoints =>
            {
                // �������� ������� ��������, ���� �� ������� ������
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
