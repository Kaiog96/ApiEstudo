﻿
namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Data.VO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileBusinessImplementation(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);

            var baseUrl = this._httpContextAccessor.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);

                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);

                    await file.CopyToAsync(stream);
                }
            }
            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }

            return list;
        }
    }
}
