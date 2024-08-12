
namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public class CompanyAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal ReferencePrice { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal PER { get; set; }
        public decimal DayVariation { get; set; }
        public decimal EPS { get; set; }
        public int Status { get; set; }
    }
}
