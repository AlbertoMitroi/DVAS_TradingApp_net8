
using InternshipTradingApp.CompanyInventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    internal class CompanyRepository(CompanyDbContext dbContext) : ICompanyRepository
    {
        public async Task<Company> Add(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            await dbContext.Companies.AddAsync(company);
            await SaveChanges();
            return company;
        }

        public async Task Delete(int companyId)
        {
            var company = await dbContext.Companies.FindAsync(companyId);
            if (company != null)
            {
                dbContext.Companies.Remove(company);
                await SaveChanges();
            }
        }

        public async Task<Company> GetById(int id)
        {
            var company = await dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                throw new Exception("Id not found");
            }
            return company;
        }

        public async Task<Company> Update(Company company)
        {
            dbContext.Companies.Update(company);
            await SaveChanges();
            return company;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
