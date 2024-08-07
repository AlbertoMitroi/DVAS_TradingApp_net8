using ExternalDataSynchronization.Domain.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Features.Extract
{
    public class ExtractZipFileCommandHandler
    {
        private readonly IExternalDataRepository externalDataRepository;
        public ExtractZipFileCommandHandler(IExternalDataRepository externalDataRepository)
        {
            this.externalDataRepository = externalDataRepository;
        }

        public async Task Handle(ExtractZipFileCommand command)
        {
            await this.externalDataRepository.ExtractZipFileAsync(command.zipFilePath, command.extractionFilePath);
        }
    }
}
