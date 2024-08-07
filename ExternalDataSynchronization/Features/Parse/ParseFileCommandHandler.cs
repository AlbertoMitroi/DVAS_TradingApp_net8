

using ExternalDataSynchronization.Domain.ExternalData;
using ExternalDataSynchronization.Models;
using Newtonsoft.Json;

namespace ExternalDataSynchronization.Features.Parse
{
    public class ParseFileCommandHandler
    {
        private readonly IExternalDataRepository externalDataRepository;

        public ParseFileCommandHandler(IExternalDataRepository externalDataRepository)
        {
            this.externalDataRepository = externalDataRepository;
        }

        public async Task<IEnumerable<ExternalDataDTO>> Handle(ParseFileCommand command)
        {
            IEnumerable<ExternalData> externalData = await this.externalDataRepository.ParseFileAsync(command.filePath);

            IEnumerable<ExternalDataDTO> externalDataDtos = externalData.Select(ExternalDataDTO.ToDto);

            return externalDataDtos;
        }
    }
}
