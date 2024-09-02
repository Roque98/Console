using FolderView.Dapper;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDirectoryRepository, DirectorioRepositorio>();
builder.Services.AddScoped<IArchivoRepository, ArchivoRepositorio>();
// Agregar servicios para las entidades de CodeGeneartor
builder.Services.AddScoped<ITipoProyectoRepository, TipoProyectoRepository>();
builder.Services.AddScoped<ITipoArchivoRepository, TipoArchivoRepository>();
builder.Services.AddScoped<ITipoArchivoTipoProyectoRepository, TipoArchivoTipoProyectoRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<IPromptsRepository, PromptsRepositorio>();
builder.Services.AddScoped<IInputPromptRepository, InputPromptRepositorio>();
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
