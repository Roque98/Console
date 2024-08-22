using FolderView.Dapper;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDirectoryRepository, DirectorioRepositorio>();
builder.Services.AddScoped<IArchivoRepository, ArchivoRepositorio>();
// Agregar servicios para las entidades de TipoProyecto
builder.Services.AddScoped<ICodeGeneratorRepository, CodeGeneratorRepository>();
builder.Services.AddControllers();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
