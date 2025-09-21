using _23WebC_Nhom10.Models;
using Serilog;

namespace _23WebC_Nhom10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            /*
            Tên: Phạm Quốc Khánh
            Date:18-09-2025
             */
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File("requsest.log") // Ghi log ra file request.log
              .CreateLogger();

            builder.Services.AddControllersWithViews();
            
            builder.Services.AddScoped<UserStore>();
            /*
             * Tên: Phạm Khắc Tuyên
             * Date:19-8-2025
             */
            var app = builder.Build();

           
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.MapStaticAssets();
            app.UseMiddleware<Middleware.Request_Log_Middleware>();
            app.UseMiddleware<Middleware.User_Load_Middleware>();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            /*
             * Tên: Bùi Huy Khang
             * Date:18-09-2025
             * Route NotFound page
             */
            app.MapFallbackToController("NotFound", "Home");
            app.Run();
        }
    }
}
