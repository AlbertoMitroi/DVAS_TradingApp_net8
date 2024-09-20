using InternshipTradingApp.CompanyInventory.Domain;

namespace InternshipTradingApp.CompanyInventory.Features.Add
{
    public class AddOrUpdateCompaniesCommandHandler(ICompanyRepository repository, IQueryCompanyRepository queryCompanyRepository)
    {
        public async Task<List<Company>> Handle(AddOrUpdateCompaniesCommand command)
        {
            var companiesMap = command.companies.ToDictionary(c => c.Symbol, c => c);

            var existingCompanies = await queryCompanyRepository.GetCompaniesBySymbols(companiesMap.Keys);
            var processedCompanies = new List<Company>();

            foreach (var existingCompany in existingCompanies)
            {
                var newData = companiesMap[existingCompany.Symbol];
                existingCompany.UpdateTradingData(newData);
                processedCompanies.Add(existingCompany);

                companiesMap.Remove(existingCompany.Symbol);
            }

            if (companiesMap.Any())
            {
                foreach (var newCompany in companiesMap.Values)
                {
                    await repository.Add(newCompany);
                    processedCompanies.Add(newCompany);
                }
            }

            if (processedCompanies.Any())
            {
                await repository.SaveChanges();
            }

            return processedCompanies;
        }

    }
}
