namespace ExternalDataSynchronization.Features.Extract
{
    public class ExtractZipFileCommand
    {
        public string zipFilePath { get; }
        public string extractionFilePath { get; }
        public ExtractZipFileCommand(string zipFilePath, string extractionFilePath)
        {
            this.zipFilePath = zipFilePath;
            this. extractionFilePath = extractionFilePath;
        }
    }
}