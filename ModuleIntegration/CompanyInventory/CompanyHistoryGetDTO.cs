namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public class CompanyHistoryGetDTO
    {
        public ulong Id { get; set; }
        public string CompanySymbol {  get; set; }=string.Empty;
        public decimal Price { get; set; }
        public decimal ReferencePrice { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal PER { get; set; }
        public decimal DayVariation { get; set; }
        public decimal EPS { get; set; }

        public DateOnly Date { get; set; }
        public decimal Volume { get; set; }
    }
}
