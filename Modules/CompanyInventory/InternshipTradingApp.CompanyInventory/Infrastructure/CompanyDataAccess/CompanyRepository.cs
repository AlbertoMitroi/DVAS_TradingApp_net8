﻿
using InternshipTradingApp.CompanyInventory.Domain;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    internal class CompanyRepository : ICompanyRepository
    {
        public async Task<Company> Add(Company company)
        {
            return company;
        }

        public async Task Delete(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Company>> GetExistingCompanies(IEnumerable<int> filterList)
        {
            return new List<Company>();
        }

        public async Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> Update(Company company)
        {
            throw new NotImplementedException();
        }
        
    }
}
