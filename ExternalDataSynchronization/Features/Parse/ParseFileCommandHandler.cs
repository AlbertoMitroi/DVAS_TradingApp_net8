using ExternalDataSynchronization.Domain.ExternalData;

namespace ExternalDataSynchronization.Features.Parse
{
    public class ParseFileCommandHandler
    {
        private readonly IExternalDataRepository externalDataRepository;

        public ParseFileCommandHandler(IExternalDataRepository externalDataRepository)
        {
            this.externalDataRepository = externalDataRepository;
        }

        public async Task<IEnumerable<ExternalData>> Handle(ParseFileCommand command)
        {
            IEnumerable<ExternalData> externalData = await this.externalDataRepository.ParseFileAsync(command.filePath);

            return externalData;
        }
    }
}
