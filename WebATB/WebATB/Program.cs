using Microsoft.EntityFrameworkCore;
using WebATB.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppATBDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyConnectionATB")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}")
    .WithStaticAssets();

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
Directory.CreateDirectory(dir);
//���������� ������ �� ����� � ����� images �� ����� /images
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(dir),
    RequestPath = "/images"
});


app.Run();
