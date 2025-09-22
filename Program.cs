using Microsoft.Extensions.Configuration;
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
            /*
             * Tên: Hồ Văn Khang nè
             * Date: 21-09-2025
             */
            IConfiguration configuration = new ConfigurationBuilder()//
            .AddJsonFile("SettingUsers.json", optional: false, reloadOnChange: true)//optional: false = báo lỗi nếu không tìm thấy tên file, reloadOnChange: true = tự động đọc dữ liệu lại khi có thay đổi
            .Build();
            string MaxSize = configuration["Max_File_Size"];
            var Ip_Ban_Array = configuration.GetSection("IP:IP_Ban").Get<string[]>();
            Console.WriteLine($"Dung lượng tối đa của một file: {MaxSize}");
            Console.WriteLine("Ip bị chặn nà:");
            if (Ip_Ban_Array != null)
            {
                foreach (var ip in Ip_Ban_Array)
                {
                    Console.WriteLine(ip);
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy danh sách IP bị chặn.");
            }
            app.Run();
        }
    }
}
