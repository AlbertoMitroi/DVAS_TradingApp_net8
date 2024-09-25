using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;

namespace InternshipTradingApp.CompanyInventory.Domain
{
    public class Company
    {
        public enum CompanyStatus
        {
            OnTheMarket = 0,
            Suspended = 1
        }

        public int Id { get; private set; }
        public string Name { get;  set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;
        public CompanyStatus Status { get; private set; }
        public ICollection<CompanyHistoryEntry> CompanyHistoryEntries { get; set; } = new List<CompanyHistoryEntry>();

        public Company() { }

        public static Company Create(string name, string symbol)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 100)
                throw new ArgumentException("Name cannot be null, empty, or exceed 100 characters.", nameof(name));

            if (string.IsNullOrEmpty(symbol) || symbol.Length > 10)
                throw new ArgumentException("Symbol cannot be null, empty, or exceed 10 characters.", nameof(symbol));

            return new Company
            {
                Name = name,
                Symbol = symbol,
                Status = CompanyStatus.OnTheMarket
            };
        }

        public void UpdateTradingData(Company newCompanyData)
        {
            if (newCompanyData == null)
                throw new ArgumentNullException(nameof(newCompanyData));
            Status = newCompanyData.Status;
        }

        public void UpdateTradingStatus(CompanyStatus newStatus)
        {
            if (Status == newStatus)
                throw new InvalidOperationException("The new status is the same as the current status.");

            Status = newStatus;
        }

    }
}
