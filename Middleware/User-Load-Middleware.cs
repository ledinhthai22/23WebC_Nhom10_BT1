using _23WebC_Nhom10.Models;
using ClosedXML.Excel; 
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
namespace _23WebC_Nhom10.Middleware
{
    public class User_Load_Middleware
    {
        private readonly RequestDelegate _next;
        public User_Load_Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "DanhSach.xlsx");
            var Users = new List<UserModel>();
            if (File.Exists(filepath))
            {
                using var workbook = new XLWorkbook(filepath);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    Users.Add(new UserModel
                    { 
                         Username = row.Cell(1).GetString(),
                         Password = row.Cell(2).GetString(),
                         Role = row.Cell(3).GetValue<int>()
                    });
                }
            }
            var userStore = context.RequestServices.GetRequiredService<UserStore>();
            userStore.Users = Users;
            /*
             * Tên: Lê Đình Thái 
             * Date:19/9/2025
             * Middleware readfile -> load list user
             */
            await _next(context);
        }
    }
}