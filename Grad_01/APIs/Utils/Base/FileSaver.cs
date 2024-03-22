using System;
using Microsoft.AspNetCore.Http;

namespace APIs.Utils.Base
{
	public class FileSaver
	{
		public FileSaver()
		{
		}

		public string FileSaveAsync(IFormFile file, string filePath)
		{
            try
            {
                string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                string route = Path.Combine("/Users/phamhung/Documents/S8/Grad/Project/Group work/Trung/repo/b-connect", filePath);

                if (!Directory.Exists(route))
                {
                    Directory.CreateDirectory(route);
                }

                string fileRoute = Path.Combine(route, filename);


                using (FileStream fs = File.Create(fileRoute))
                {
                        file.OpenReadStream().CopyTo(fs);
                }
                string result = Path.Combine(filePath, filename);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void FileDelete(string path)
        {
            try
            {
               string route = Path.Combine("/Users/phamhung/Documents/S8/Grad/Project/Group work/Trung/repo/b-connect", path);
                if (File.Exists(route))
                {
                    File.Delete(route);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
	}
}

