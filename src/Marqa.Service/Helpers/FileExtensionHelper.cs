namespace Marqa.Service.Helpers;

public static class FileExtensionHelper
{
    private static readonly HashSet<string> _imageExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp",
            ".tiff", ".tif", ".svg", ".ico", ".heic", ".heif",
            ".raw", ".cr2", ".nef", ".dng", ".arw"
        };

    private static readonly HashSet<string> _videoExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".avi", ".mov", ".mkv", ".webm", ".wmv",
            ".flv", ".m4v", ".mpg", ".mpeg", ".3gp", ".ogg",
            ".ogv", ".qt", ".rm", ".rmvb", ".asf", ".swf"
        };

    public static bool IsImageExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return false;

        return _imageExtensions.Contains(Normalize(extension));
    }

    public static bool IsVideoExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return false;

        return _videoExtensions.Contains(Normalize(extension));
    }

    private static string Normalize(string extension)
    {
        extension = extension.Trim().ToLowerInvariant();
        return extension.StartsWith('.') ? extension : $".{extension}";
    }
}
