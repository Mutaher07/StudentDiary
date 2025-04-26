using Microsoft.EntityFrameworkCore;
using StudentDiary.Data;
using StudentDiary.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext �� SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();

// 2) �������� MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// 3) �������� Razor Pages (��� � ����������)
builder.Services.AddRazorPages();

// 4) �������� �� Authorization ������
builder.Services.AddAuthorization();

var app = builder.Build();

// 5) ����������� ��������
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // �� ������� ��������, ��� ��� ����
}

// 6) Middleware �� ��������� �� ������ � ������������ �����
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 7) Middleware �� Authorization
app.UseAuthorization();

// 8) ���������� ������������� �� ���������� (MVC)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

// 9) ���������� ������� �� Razor Pages
app.MapRazorPages();

// ���������� �� ������������
app.Run();
