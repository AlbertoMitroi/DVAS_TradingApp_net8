using ExternalDataSynchronization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Features.Post
{
    public class PostCommand
    {
        public string url {  get; } = string.Empty;
        public IEnumerable<ExternalDataDTO> externalDataDTOs { get; }

        public PostCommand(string url, IEnumerable<ExternalDataDTO> externalDataDTOs)
        {
            this.url = url;
            this.externalDataDTOs = externalDataDTOs;
        }
    }
}
