using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Services
{
    public interface ISystemService
    {
        List<string> UploadIForm(List<IFormFile> files, string productType, string productName);
        string UploadFormReturnBase64(List<IFormFile> files);
    }
    public class SystemService:ISystemService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public SystemService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<string> UploadIForm(List<IFormFile> files,string productType,string productName)
        {
            List<string> filenames = new List<string>();
            try
            {
                
                foreach (var file in files)
                {
                    var fileName = file.FileName;
                    fileName = $"/UploadFile/{productType}/{productName}/{fileName}";
                    filenames.Add(fileName);
                    var FilePath = _hostingEnvironment.ContentRootPath + $@"/UploadFile/{productType}/{productName}/";
                    fileName = _hostingEnvironment.ContentRootPath + fileName;
                    DirectoryInfo FileInfo = new DirectoryInfo(FilePath);
                    if (!FileInfo.Exists) { FileInfo.Create(); }
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return filenames;
        }

        public string UploadFormReturnBase64(List<IFormFile> files)
        {
            byte[] arr=null;
            var fileExtension = string.Empty;
            foreach (var item in files)
            {
                fileExtension=Path.GetExtension(item.FileName);
                Stream file = item.OpenReadStream();
                arr = new byte[file.Length];
                file.Position = 0;
                file.Read(arr, 0, (int)file.Length);
            }
            if (fileExtension.Equals("jpg"))
            {
                fileExtension = "jpeg";
            }
            var baseName = $"data:image/{fileExtension};base64,";
            return $"{baseName}{Convert.ToBase64String(arr)}";
        }

    }
}
