using FolderView.Dapper;
using FolderView.Dapper.Repositorios;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.CodeGenerator.Repositorios;
using FolderView.Dapper.AdministracionBot.Interfaces;
using FolderView.Dapper.AdministracionBot.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDirectoryRepository, DirectorioRepositorio>();
builder.Services.AddScoped<IArchivoRepository, ArchivoRepositorio>();
// Agregar servicios para las entidades de TipoProyecto
builder.Services.AddScoped<ITipoProyectoRepository, TipoProyectoRepositorio>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepositorio>();
builder.Services.AddScoped<ICodeGeneratorIArchivoRepository, CodeGeneratorArchivoRepositorio>();
builder.Services.AddScoped<IPromptTemplateRepository, PromptTemplateRepositorio>();
builder.Services.AddScoped<IParametrosPromptTemplateRepository, ParametrosPromptTemplateRepositorio>();

// Agregar servicios para el Módulo de Administración del Bot
builder.Services.AddScoped<IDashboardRepository, DashboardRepositorio>();
builder.Services.AddScoped<IModuloRepository, ModuloRepositorio>();
builder.Services.AddScoped<IOperacionRepository, OperacionRepositorio>();
builder.Services.AddScoped<IRolRepository, RolRepositorio>();
builder.Services.AddScoped<ILogOperacionRepository, LogOperacionRepositorio>();
builder.Services.AddScoped<IUsuarioTelegramRepository, UsuarioTelegramRepositorio>();
builder.Services.AddScoped<IKnowledgeRepository, KnowledgeRepositorio>();
builder.Services.AddScoped<IRolIARepository, RolIARepositorio>();

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
