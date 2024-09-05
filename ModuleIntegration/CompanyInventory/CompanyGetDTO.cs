namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public class CompanyGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public int Status { get; set; }
        public List<CompanyHistoryGetDTO> History { get; set; }
    }
}
