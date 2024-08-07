using ExternalDataSynchronization.Domain.ExternalData;

namespace ExternalDataSynchronization.Models
{
    public class ExternalDataDTO
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Close { get; set; }
        public decimal Change { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Avg { get; set; }
        public decimal Volume { get; set; }
        public decimal NoOfTrades { get; set; }
        public decimal High52 { get; set; }
        public decimal Low52 { get; set; }

        public static ExternalDataDTO ToDto(ExternalData externalData)
        {
            return new ExternalDataDTO
            {
                Symbol = externalData.Symbol,
                CompanyName = externalData.CompanyName,
                Close = ConvertToDecimal(externalData.Close),
                Change = ConvertToDecimal(externalData.Change),
                Open = ConvertToDecimal(externalData.Open),
                High = ConvertToDecimal(externalData.High),
                Low = ConvertToDecimal(externalData.Low),
                Avg = ConvertToDecimal(externalData.Avg),
                Volume = ConvertToDecimal(externalData.Volume),
                NoOfTrades = ConvertToDecimal(externalData.NoOfTrades),
                High52 = ConvertToDecimal(externalData.High52),
                Low52 = ConvertToDecimal(externalData.Low52)
            };
        }
        private static decimal ConvertToDecimal(string value)
        {
            if (decimal.TryParse(value, out var result))
            {
                return result;
            }
            return 0m;
        }
    }
}
