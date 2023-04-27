using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var stringConexao = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<BancoContext>(
                options => options.UseMySql(stringConexao, ServerVersion.Parse("8.1.32"))
            );

            //ATEN��O: Necessita iniciar os servi�os do sistema. Caso contr�rio, o sistema n�o ir� rodar. 
            builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}