using ExternalDataSynchronization.Domain.ExternalData;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System.Runtime.CompilerServices;

namespace ExternalDataSynchronization.Models
{
    public class ExternalHistoryDataDTO
    {
        private static readonly Random _random = new Random();

        public static CompanyHistoryGetDTO ToDto(ExternalData externalHistoryData)
        {
            string companySymbol=externalHistoryData.Symbol;
            decimal closePrice = ConvertToDecimal(externalHistoryData.Close);
            decimal variation = (decimal)_random.NextDouble() * 0.10m - 0.05m;
            decimal roundedVariation = Math.Round(variation, 4);
            decimal randomPrice = Math.Round(closePrice * (1 + roundedVariation), 4);


            return new CompanyHistoryGetDTO
            {
                CompanySymbol = companySymbol,
                Price = randomPrice,
                ReferencePrice = ConvertToDecimal(externalHistoryData.Close),
                OpeningPrice = ConvertToDecimal(externalHistoryData.Open),
                ClosingPrice = ConvertToDecimal(externalHistoryData.Close),
                EPS = ConvertToDecimal(externalHistoryData.Avg)
            };
        }
       
        private static decimal ConvertToDecimal(string value)
        {
            if (decimal.TryParse(value, out var result))
            {
                return Math.Round(result, 4);
            }
            return 0m;
        }
    }
}
