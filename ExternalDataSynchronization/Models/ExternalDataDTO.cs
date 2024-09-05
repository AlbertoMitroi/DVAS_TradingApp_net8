using ExternalDataSynchronization.Domain.ExternalData;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace ExternalDataSynchronization.Models
{
    public class ExternalDataDTO 
    {

        public static CompanyGetDTO ToDto(ExternalData externalData)
        {

            return new CompanyGetDTO
            {
                Symbol = externalData.Symbol,
                Name = externalData.CompanyName
            };
        }

    }
}
