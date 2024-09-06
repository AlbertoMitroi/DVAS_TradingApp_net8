﻿
namespace InternshipTradingApp.CompanyInventory.Domain
{
    public interface IQueryCompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<IEnumerable<Company>> GetCompaniesBySymbols(IEnumerable<string> symbols);
        Task<IEnumerable<Company>> GetAllCompaniesHistory(string symbol);
        Task<Company?> GetCompanyBySymbol(string companySymbol);
    }
}
