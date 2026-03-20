using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("AReyesDiciembre") ??
     throw new InvalidOperationException("Connection string 'AReyesDiciembre'" +
    " not found.");
builder.Services.AddDbContext<DL.AreyesDiciembreContext>(options =>
    options.UseSqlServer(conString));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Direccion>();
builder.Services.AddScoped<BL.Colonia>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=GetALl}/{id?}");

app.Run();
