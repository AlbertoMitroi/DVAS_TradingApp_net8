
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;

namespace InternshipTradingApp.CompanyInventory.Domain
{ 
    public interface ICompanyRepository
    {
        Task<Company> GetById(int id);
        Task<Company> Add(Company company);
        Task<Company> Update(Company company);
        Task Delete(int companyId);
        Task SaveChanges();
    }
}
