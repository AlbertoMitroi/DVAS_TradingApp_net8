
using ExternalDataSynchronization.Models;

namespace ExternalDataSynchronization.Domain.ExternalData
{
    public interface IExternalDataRepository
    {
        Task DownloadExternalDataAsync(string downloadUrl, string downloadLocationPath);
        Task ExtractZipFileAsync(string zipFilePath, string extractionFilePath);
        Task<IEnumerable<ExternalData>> ParseFileAsync(string filePath);
        Task PostDataApiAsync(string url, IEnumerable<ExternalDataDTO> externalDataDto);
    }
}
