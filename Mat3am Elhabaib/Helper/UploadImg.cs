namespace Mat3am_Elhabaib.Helper
{
    public class UploadImg
    {
        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                // Set the base directory to wwwroot
                string wwwRootPath = Directory.GetCurrentDirectory() + "/wwwroot";

                // Get the target folder path
                string FolderPath = Path.Combine(wwwRootPath, "ImgItem", FolderName);

                // Create the directory if it doesn't exist
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                // Get unique file name using GUID
                string FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);

                // Combine path and file name
                string FinalPath = Path.Combine(FolderPath, FileName);

                // Save file
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }

                return FileName; // Return the file name (which will be stored in the database)
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string RemoveFile(string FolderName, string fileName)
        {

            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName, fileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }

                return "File Not Deleted";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
