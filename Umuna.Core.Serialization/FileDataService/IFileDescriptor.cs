namespace Umuna.Core.Services.FileDataService
{
    public interface IFileDescriptor
    {
        string FileDirectory { get; }
        string FileExtension { get; }
        string FileName { get; }
        string FilePath { get; }
    }
}