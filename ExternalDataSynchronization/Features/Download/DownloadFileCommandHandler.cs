using ExternalDataSynchronization.Domain.ExternalData;

namespace ExternalDataSynchronization.Features.Download
{
    internal class DownloadFileCommandHandler
    {
        private readonly IExternalDataRepository externalDataRepository;

        public DownloadFileCommandHandler(IExternalDataRepository externalDataRepository)
        {
            this.externalDataRepository = externalDataRepository;
        }

        public async Task Handle(DownloadFileCommand command)
        {
            await this.externalDataRepository.DownloadExternalDataAsync(command.DownloadUrl, command.DownloadLocationPath);
        }
    }
}
