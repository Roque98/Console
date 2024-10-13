namespace FolderView.Dapper.Utils
{
    public static class CodeGeneratorUtil
    {
        public static string EscapeHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");
        }

    }
}
