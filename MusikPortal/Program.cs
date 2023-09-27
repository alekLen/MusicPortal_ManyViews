using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// качаем NuGet Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
// качаем NuGet Package Microsoft.EntityFrameworkCore.SqlServer на третьем уровне для работы с БД
// качаем NuGet Package AutoMapper на втором уровне для преобразования классов моделей в классы DTO
// качаем NuGet Package BCrypt.Net-Next на втором уровне для хеширования
builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IStyleService, StyleService>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISaltService, SaltService>();

// добавляем сервис для динамического создания вьюшек
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Длительность сеанса (тайм-аут завершения сеанса)
    options.Cookie.Name = "Session"; // Каждая сессия имеет свой идентификатор, который сохраняется в куках.

});
// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
