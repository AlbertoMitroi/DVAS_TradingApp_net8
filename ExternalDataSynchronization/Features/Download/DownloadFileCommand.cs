
namespace ExternalDataSynchronization.Features.Download
{
    public class DownloadFileCommand
    {
        public string DownloadUrl { get; }
        public string DownloadLocationPath { get; }

        public DownloadFileCommand(string downloadUrl, string downloadLocationPath)
        {
            DownloadUrl = downloadUrl;
            DownloadLocationPath = downloadLocationPath;
        }
    }
}
