﻿
namespace InternshipTradingApp.CompanyInventory.Domain
{
    public interface IQueryCompanyRepository
    {
        Task<IQueryable<Company>> GetAllCompanies();
        Task<IEnumerable<Company>> GetCompaniesBySymbols(IEnumerable<string> symbols);
        Task<Company?> GetCompanyBySymbol(string companySymbol);
    }
}