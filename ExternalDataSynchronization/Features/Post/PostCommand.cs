using ExternalDataSynchronization.Models;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
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
        public string historyUrl {  get; } = string.Empty;
        public IEnumerable<CompanyGetDTO> externalDataDTOs { get; }

        public IEnumerable<CompanyHistoryGetDTO> externalHistoyDataDTOs { get; }


        public PostCommand(string url,string historyUrl, IEnumerable<CompanyGetDTO> externalDataDTOs, IEnumerable<CompanyHistoryGetDTO> externalHistoyDataDTOs)
        {
            this.url = url;
            this.historyUrl = historyUrl;
            this.externalDataDTOs = externalDataDTOs;
            this.externalHistoyDataDTOs = externalHistoyDataDTOs;
            
        }
    }
}
