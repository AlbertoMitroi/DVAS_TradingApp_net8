using ExternalDataSynchronization.Domain.ExternalData;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace ExternalDataSynchronization.Models
{
    public class ExternalDataDTO : CompanyGetDTO
    {
        private static readonly Random _random = new Random();

        public static ExternalDataDTO ToDto(ExternalData externalData)
        {
            decimal closePrice = ConvertToDecimal(externalData.Close);
            decimal variation = (decimal)_random.NextDouble() * 0.10m - 0.05m;
            decimal roundedVariation = Math.Round(variation, 4);
            decimal randomPrice = Math.Round(closePrice * (1 + roundedVariation), 4);

            return new ExternalDataDTO
            {
                Symbol = externalData.Symbol,
                Name = externalData.CompanyName,
                Price = randomPrice,
                ReferencePrice = ConvertToDecimal(externalData.Close),
                OpeningPrice = ConvertToDecimal(externalData.Open),
                ClosingPrice = ConvertToDecimal(externalData.Close),
                EPS = ConvertToDecimal(externalData.Avg)
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
