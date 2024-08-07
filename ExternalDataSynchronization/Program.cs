using ExternalDataSynchronization.Domain.ExternalData;
using ExternalDataSynchronization.Features.Download;
using ExternalDataSynchronization.Features.Extract;
using ExternalDataSynchronization.Features.Parse;
using ExternalDataSynchronization.Features.Shared;
using ExternalDataSynchronization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IExternalDataRepository, ExternalDataRepository>()
            .AddTransient<DownloadFileCommandHandler>()
            .AddTransient<ExtractZipFileCommandHandler>()
            .AddTransient<ParseFileCommandHandler>()
            .BuildServiceProvider();

        string downloadUrl = HelperMethods.GetUrlForYesterday();
        string downloadLocationPath = HelperMethods.GetDownloadLocationPath();
        string zipFilePath = Path.Combine(downloadLocationPath, "dataArchived.zip");
        string extractionFilePath = Path.Combine(downloadLocationPath, "dataExtracted");
        string xlsxFilePath = HelperMethods.GetXlsxFilePath(extractionFilePath);

        var downloadCommand = new DownloadFileCommand(downloadUrl, zipFilePath);
        var downloadCommandHandler = serviceProvider.GetService<DownloadFileCommandHandler>();

        var extractionCommand = new ExtractZipFileCommand(zipFilePath, extractionFilePath);
        var extractionCommandHandler = serviceProvider.GetService<ExtractZipFileCommandHandler>();

        var parseCommand = new ParseFileCommand(xlsxFilePath);
        var parseCommandHandler = serviceProvider.GetService<ParseFileCommandHandler>();

        if (downloadCommandHandler == null)
        {
            Console.WriteLine("Failed to resolve DownloadFileCommandHandler.");
            return;
        }
        if (extractionCommandHandler == null)
        {
            Console.WriteLine("Failed to resolve ExtractZipFileCommandHandler.");
            return;
        }
        if (parseCommandHandler == null)
        {
            Console.WriteLine("Failed to resolve ParseFileCommandHandler.");
            return;
        }

        await downloadCommandHandler.Handle(downloadCommand);
        await extractionCommandHandler.Handle(extractionCommand);
        var externalDataDto = await parseCommandHandler.Handle(parseCommand);

        Console.WriteLine("program.cs: " + externalDataDto.Count());
            
    }
}
