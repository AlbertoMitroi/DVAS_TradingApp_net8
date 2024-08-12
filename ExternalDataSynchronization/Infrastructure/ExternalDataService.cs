using ExternalDataSynchronization.Domain.ExternalData;
using ExternalDataSynchronization.Features.Download;
using ExternalDataSynchronization.Features.Extract;
using ExternalDataSynchronization.Features.Parse;
using ExternalDataSynchronization.Features.Post;
using ExternalDataSynchronization.Features.Shared;
using ExternalDataSynchronization.Infrastructure;
using ExternalDataSynchronization.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipTradingApp.CompanyInventory.Infrastructure
{
    public class ExternalDataService
    {
        public static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<IExternalDataRepository, ExternalDataRepository>()
                .AddTransient<DownloadFileCommandHandler>()
                .AddTransient<ExtractZipFileCommandHandler>()
                .AddTransient<ParseFileCommandHandler>()
                .AddTransient<PostCommandHandler>()
                .BuildServiceProvider();
        }

        public static async Task<IEnumerable<ExternalData>> ExecuteCommandsAsync(IServiceProvider serviceProvider)
        {
            string downloadUrl = HelperMethods.GetUrlForYesterday();
            string downloadLocationPath = HelperMethods.GetDownloadLocationPath();
            string zipFilePath = Path.Combine(downloadLocationPath, "dataArchived.zip");
            string extractionFilePath = Path.Combine(downloadLocationPath, "dataExtracted");
            string xlsxFilePath = HelperMethods.GetXlsxFilePath(extractionFilePath);
            string postUrl = "https://localhost:7221/api/CompanyInventory/external-data";

            try
            {
                await DownloadFileAsync(serviceProvider, downloadUrl, zipFilePath);
                await ExtractFileAsync(serviceProvider, zipFilePath, extractionFilePath);
                var externalData = await ParseFileAsync(serviceProvider, xlsxFilePath);
                var externalDataDtos = externalData.Select(ExternalDataDTO.ToDto);


                await PostDataAsync(serviceProvider, postUrl, externalDataDtos);

                return externalData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An exception occurred while processing the commands.\nException message: {ex.Message}");
                return Enumerable.Empty<ExternalData>();
            }
        }

        private static async Task DownloadFileAsync(IServiceProvider serviceProvider, string downloadUrl, string zipFilePath)
        {
            var downloadCommandHandler = GetService<DownloadFileCommandHandler>(serviceProvider);
            var downloadCommand = new DownloadFileCommand(downloadUrl, zipFilePath);
            Console.WriteLine("Starting file download...");
            await downloadCommandHandler.Handle(downloadCommand);
            Console.WriteLine("File downloaded successfully.\n");
        }

        private static async Task ExtractFileAsync(IServiceProvider serviceProvider, string zipFilePath, string extractionFilePath)
        {
            var extractionCommandHandler = GetService<ExtractZipFileCommandHandler>(serviceProvider);
            var extractionCommand = new ExtractZipFileCommand(zipFilePath, extractionFilePath);
            Console.WriteLine("Starting file extraction...");
            await extractionCommandHandler.Handle(extractionCommand);
            Console.WriteLine("File extracted successfully.\n");
        }

        private static async Task<IEnumerable<ExternalData>> ParseFileAsync(IServiceProvider serviceProvider, string xlsxFilePath)
        {
            var parseCommandHandler = GetService<ParseFileCommandHandler>(serviceProvider);
            var parseCommand = new ParseFileCommand(xlsxFilePath);
            Console.WriteLine("Starting file parsing...");
            var externalData = await parseCommandHandler.Handle(parseCommand);
            Console.WriteLine($"File parsed successfully.\nRecord count: {externalData.Count()} stocks from BVB\n");
            return externalData;
        }

        private static async Task PostDataAsync(IServiceProvider serviceProvider, string postUrl, IEnumerable<ExternalDataDTO> externalDataDtos)
        {
            var postCommandHandler = GetService<PostCommandHandler>(serviceProvider);
            var postCommand = new PostCommand(postUrl, externalDataDtos);
            Console.WriteLine("Starting data post...");
            await postCommandHandler.Handle(postCommand);
            Console.WriteLine("Data posted successfully.\n");
        }

        private static T GetService<T>(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} is not registered.");
            }
            return service;
        }
    }
}