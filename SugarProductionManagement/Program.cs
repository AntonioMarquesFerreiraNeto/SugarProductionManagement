using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
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

            //ATENÇÃO: Necessita iniciar os serviços do sistema. Caso contrário, o sistema não irá rodar. 
            builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<ISafraRepository, SafraRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IProducaoRepository, ProducaoRepository>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISection, Section>();
            builder.Services.AddScoped<IEmail, Email>();

            builder.Services.AddSession(o => {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

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

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Logar}/{action=Index}/{id?}");

            app.Run();
        }
    }
}