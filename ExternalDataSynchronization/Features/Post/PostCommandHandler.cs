using ExternalDataSynchronization.Domain.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Features.Post
{
    public class PostCommandHandler
    {
        private readonly IExternalDataRepository externalDataRepository;

        public PostCommandHandler(IExternalDataRepository externalDataRepository)
        {
            this.externalDataRepository = externalDataRepository;
        }

        public async Task Handle(PostCommand command)
        {
            await this.externalDataRepository.PostDataApiAsync(command.url,command.historyUrl, command.externalDataDTOs,command.externalHistoyDataDTOs);
           
        }
    }
}
