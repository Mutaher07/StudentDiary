using Microsoft.EntityFrameworkCore;
using StudentDiary.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext за SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Добавяме MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// 3) Добавяме Razor Pages (ако е необходимо)
builder.Services.AddRazorPages();

// 4) Добавяне на Authorization услуги
builder.Services.AddAuthorization();

var app = builder.Build();

// 5) Автоматични миграции
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Ще извърши миграции, ако има нови
}

// 6) Middleware за обработка на грешки в продукционна среда
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 7) Middleware за Authorization
app.UseAuthorization();

// 8) Стандартна маршрутизация за контролери (MVC)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

// 9) Стандартен маршрут за Razor Pages
app.MapRazorPages();

// Стартиране на приложението
app.Run();
