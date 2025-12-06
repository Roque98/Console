using FolderView.Dapper;
using FolderView.Dapper.Repositorios;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.CodeGenerator.Repositorios;
using FolderView.Dapper.AdministracionBot.Interfaces;
using FolderView.Dapper.AdministracionBot.Repositorios;
using FolderView.Dapper.ConfiguracionIA.Interfaces;
using FolderView.Dapper.ConfiguracionIA.Repositorios;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;
using FolderView.Dapper.SmartPromptLibrary.Repositorios;
using FolderView.Services;
using FolderView.Services.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>();

// Agregar servicio de cifrado (Singleton para mejor rendimiento)
builder.Services.AddSingleton<IEncryptionService, AesEncryptionService>();

// Registrar HttpClient Factory (requerido por servicios LLM)
builder.Services.AddHttpClient();

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
builder.Services.AddScoped<IIconoRepository, IconoRepositorio>();

// Agregar servicios para el Módulo de Configuración de IA
builder.Services.AddScoped<IProveedorIARepository, ProveedorIARepositorio>();
builder.Services.AddScoped<IModeloIARepository, ModeloIARepositorio>();
builder.Services.AddScoped<IConfiguracionIAModuloRepository, ConfiguracionIAModuloRepositorio>();
builder.Services.AddScoped<ISistemaAplicacionRepository, SistemaAplicacionRepositorio>();

// Registrar LLM Providers (librerías para consultas a IA)
builder.Services.AddScoped<FolderView.Services.LlmProviders.Providers.OpenAIProvider>();
builder.Services.AddScoped<FolderView.Services.LlmProviders.Providers.AnthropicProvider>();
builder.Services.AddScoped<FolderView.Services.LlmProviders.Providers.GoogleGeminiProvider>();
builder.Services.AddScoped<FolderView.Services.LlmProviders.Providers.MistralProvider>();
builder.Services.AddScoped<FolderView.Services.LlmProviders.LlmProviderFactory>();

// Registrar LlmService
// OPCIÓN 1: Usar el servicio REFACTORIZADO (Recomendado - usa providers modulares)
// builder.Services.AddScoped<FolderView.Services.ILlmService, FolderView.Services.LlmServiceRefactored>();

// OPCIÓN 2: Usar el servicio ORIGINAL (Legacy - tiene lógica directa de OpenAI/Anthropic)
builder.Services.AddScoped<FolderView.Services.ILlmService, FolderView.Services.LlmService>();

// Registrar PdfService (para procesamiento de archivos PDF)
builder.Services.AddScoped<FolderView.Services.IPdfService, FolderView.Services.PdfService>();

// Agregar servicios para el Módulo SmartPromptLibrary
builder.Services.AddScoped<ICategoriaPromptRepository, CategoriaPromptRepositorio>();
builder.Services.AddScoped<IEtiquetaPromptRepository, EtiquetaPromptRepositorio>();
builder.Services.AddScoped<IPromptRepository, PromptRepositorio>();
builder.Services.AddScoped<IEjecucionPromptRepository, EjecucionPromptRepositorio>();
builder.Services.AddScoped<IVersionPromptRepository, VersionPromptRepositorio>();

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
