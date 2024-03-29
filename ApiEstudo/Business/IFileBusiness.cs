namespace ApiEstudo.Business
{
    using ApiEstudo.Data.VO;

    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);

        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
