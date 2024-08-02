namespace InternshipTradingApp.CompanyInventory.Domain
{ 
    internal interface ICompanyRepository
    {
        Task<Company> GetById(int id);
        Task<Company> Add(Company company);
        Task<Company> Update(Company company);
        Task Delete(int companyId);

        Task<IEnumerable<Company>> GetExistingCompanies(IEnumerable<int> filterList);

        Task SaveChanges();
    }
}
