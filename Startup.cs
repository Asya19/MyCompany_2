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

            // ���������� �������� ��

            // ����������� identity �������

            // ����������� authentication cookie

            // 

            // 


            // ��������� ��������� ������������ � ������������� (MVC)
            services.AddControllersWithViews()
                // ���������� ������������� � asp.net core 3.0
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseRouting();

            // ���������� ��������� ��������� ������ � ���������� (css, js � �.�.)
            app.UseStaticFiles();

            // �������������
            // ������������ ������ ��� �������� (���������)
            app.UseEndpoints(endpoints =>
            {
                // �������� ������� ��������, ���� �� ������� ������
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
