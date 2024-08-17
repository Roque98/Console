using System;
using System.Data;
using Dapper;
using FolderViewConsole.Dapper.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        // Ejemplo de cómo acceder a una configuración
        var loggingLevel = configuration["Logging:LogLevel:Default"];
        Console.WriteLine($"El nivel de registro predeterminado es: {loggingLevel}");


        string rutaDirectorio = "C:\\Users\\babyn\\Documents\\proyectos\\DriversPython"; // Puedes cambiar esta ruta por la que desees
        string rutaBasePathRoot = ObtenerRutaPadre(rutaDirectorio); // completar codigo
        DirectorioEntidad directorio = ObtenerDirectorioDesdeRuta(rutaDirectorio, rutaBasePathRoot);


        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

 

        // Insertar en la base de datos
        using (var connection = new SqlConnection(connectionString))
        {

            // Insertar en la base de datos
            InsertarDirectorioRecursivo(directorio, 0, connectionString);
        }
    }
    static void InsertarDirectorioRecursivo(DirectorioEntidad directorio, int DirectorioParentId, string connectionString)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Insertar el directorio
            var parametrosDirectorio = new
            {
                Nombre = directorio.Nombre,
                Tamano = directorio.Tamano,
                Ruta = directorio.Ruta,
                FechaCreacion = directorio.FechaCreacion,
                FechaModificacion = directorio.FechaModificacion,
                DirectorioParentID = DirectorioParentId
            };

            int directorioId = connection.QuerySingle<int>("FolderView_Directorio_Add", parametrosDirectorio, commandType: CommandType.StoredProcedure);

            // Insertar los archivos del directorio actual
            foreach (var archivo in directorio.Archivos)
            {
                archivo.DirectorioID = directorioId;
                string insertArchivoQuery = "INSERT INTO consolaMonitoreo..FolderView_Archivos (Nombre, Tamano, Ruta, FechaCreacion, FechaModificacion, Extension, DirectorioID) VALUES (@Nombre, @Tamano, @Ruta, @FechaCreacion, @FechaModificacion, @Extension, @DirectorioID)";
                connection.Execute(insertArchivoQuery, archivo);
            }

            // Insertar los subdirectorios de forma recursiva
            foreach (var subdirectorio in directorio.Subdirectorios)
            {
                InsertarDirectorioRecursivo(subdirectorio, directorioId, connectionString);
            }
        }
    }

    public static DirectorioEntidad ObtenerDirectorioDesdeRuta(string ruta, string rutaBasePathRoot)
    {
        DirectoryInfo infoDirectorio = new DirectoryInfo(ruta);
        if (!infoDirectorio.Exists)
        {
            throw new DirectoryNotFoundException($"El directorio '{ruta}' no existe.");
        }

        var directorio = new DirectorioEntidad
        {
            DirectorioID = 0, // Puedes asignar un ID si es necesario
            Nombre = infoDirectorio.Name,
            Tamano = CalcularTamanoDirectorio(infoDirectorio),
            Ruta = infoDirectorio.FullName.Replace(rutaBasePathRoot, ""),
            FechaCreacion = infoDirectorio.CreationTime,
            FechaModificacion = infoDirectorio.LastWriteTime,
            DirectorioParentID = 0, // Puedes asignar un ID si es necesario
            Archivos = new List<ArchivoEntidad>(),
            Subdirectorios = new List<DirectorioEntidad>()
        };

        // Llenar lista de archivos
        FileInfo[] archivos = infoDirectorio.GetFiles();
        foreach (var archivoInfo in archivos)
        {
            directorio.Archivos.Add(new ArchivoEntidad
            {
                ArchivoID = 0, // Puedes asignar un ID si es necesario
                Nombre = archivoInfo.Name,
                Tamano = archivoInfo.Length,
                FechaCreacion = archivoInfo.CreationTime,
                FechaModificacion = archivoInfo.LastWriteTime,
                Ruta = archivoInfo.FullName.Replace(rutaBasePathRoot, ""),
                Extension = archivoInfo.Extension,
                DirectorioID = directorio.DirectorioID // Establecer el ID del directorio al que pertenece el archivo
            });
        }

        // Llenar lista de subdirectorios
        DirectoryInfo[] subdirectorios = infoDirectorio.GetDirectories();
        foreach (var subdirectorioInfo in subdirectorios)
        {
            directorio.Subdirectorios.Add(ObtenerDirectorioDesdeRuta(subdirectorioInfo.FullName, rutaBasePathRoot));
        }

        return directorio;
    }

    public static long CalcularTamanoDirectorio(DirectoryInfo directorio)
    {
        long tamano = 0;

        FileInfo[] archivos = directorio.GetFiles();
        foreach (var archivoInfo in archivos)
        {
            tamano += archivoInfo.Length;
        }

        DirectoryInfo[] subdirectorios = directorio.GetDirectories();
        foreach (var subdirectorioInfo in subdirectorios)
        {
            tamano += CalcularTamanoDirectorio(subdirectorioInfo);
        }

        return tamano;
    }


    public static string ObtenerRutaPadre(string rutaDirectorio)
    {
        string[] partesRuta = rutaDirectorio.Split(Path.DirectorySeparatorChar);

        // Si la carpeta es la carpeta raíz, devolver la misma ruta
        if (partesRuta.Length == 1)
        {
            return rutaDirectorio;
        }

        // Eliminar el nombre de la última carpeta
        string rutaPadre = string.Join(Path.DirectorySeparatorChar.ToString(), partesRuta.Take(partesRuta.Length - 1));

        return rutaPadre;
    }


}
