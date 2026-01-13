namespace SixteenClothing.App.Extensions
{
    public static class FileExtension
    {
        public static async Task<string> UploadFile(this IFormFile file, string root, string path)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(root, path, filename);
            using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }

        public static bool IsCorrectFormat(this IFormFile file, params string[] formats)
        {
            bool result = false;
            foreach (var format in formats)
            {
                if (file.ContentType.Contains(format.ToLower())) result = true;
            }
            return result;
        }

        public static bool IsSizeOk(this IFormFile file, short maxSizeInMB)
        {
            short sizeInBytes = ((short)(maxSizeInMB * 1024 * 1024));
            if (file.Length > sizeInBytes) return false;
            return true;
        }
    }
}
