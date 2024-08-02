using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;


namespace InternshipTradingApp.CompanyInventory.Features.Add
{
    internal class AddOrUpdateCompaniesCommandHandler
    {
        private readonly ICompanyRepository companyRepository;
        public AddOrUpdateCompaniesCommandHandler(ICompanyRepository repository)
        {
            companyRepository = repository;
        }

        public async Task Handle(AddOrUpdateCompaniesCommand command)
        {

            Dictionary<int, CompanyDTO> companiesMap = new Dictionary<int, CompanyDTO>();
            foreach (var company in command.companies)
            {
                companiesMap[company.Id] = company;
            }
            var companiesToUpdate = await companyRepository
                                        .GetExistingCompanies(companiesMap.Keys);


            foreach (var existingCompany in companiesToUpdate)
            {
                var newData = companiesMap[existingCompany.Id]
                                .ToDomainObject();

                companiesMap.Remove(existingCompany.Id);
                existingCompany.UpdateTradingData(newData);
            }

            foreach (var newCompanyEntry in companiesMap)
            {
                await companyRepository.Add(newCompanyEntry.Value
                                                            .ToDomainObject());
            }

            await companyRepository.SaveChanges();

        }
    }
}
