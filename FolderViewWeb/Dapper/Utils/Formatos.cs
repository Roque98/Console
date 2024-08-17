namespace FolderView.Dapper.Utils
{
    public static class Formatos
    {
        // Método para convertir el tamaño del archivo a una representación más legible
        public static string TamanioFormateado(long TamanoEnBytes)
        {
            const double kilobyte = 1024;
            const double megabyte = kilobyte * 1024;
            const double gigabyte = megabyte * 1024;

            if (TamanoEnBytes < kilobyte)
            {
                return TamanoEnBytes + " Bytes";
            }
            else if (TamanoEnBytes < megabyte)
            {
                return (TamanoEnBytes / kilobyte).ToString("F2") + " KB";
            }
            else if (TamanoEnBytes < gigabyte)
            {
                return (TamanoEnBytes / megabyte).ToString("F2") + " MB";
            }
            else
            {
                return (TamanoEnBytes / gigabyte).ToString("F2") + " GB";
            }
        }
    }
}
