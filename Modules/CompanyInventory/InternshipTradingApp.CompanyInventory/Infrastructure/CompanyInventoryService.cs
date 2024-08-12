﻿using InternshipTradingApp.CompanyInventory.Features.Add;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory
{
    internal class CompanyInventoryService(
        GetAllCompaniesQueryHandler getAllCompaniesQueryHandler,
        GetCompanyBySymbolQueryHandler getCompanyBySymbolQueryHandler,
        AddOrUpdateCompaniesCommandHandler addOrUpdateCompaniesCommandHandler
            ) : ICompanyInventoryService
    {
        public async Task<IEnumerable<CompanyGetDTO>> GetAllCompanies()
        {
            return await getAllCompaniesQueryHandler.Handle();
        }

        public async Task<CompanyGetDTO?> GetCompanyBySymbol(string symbol)
        {
            var query = new GetCompanyBySymbolQuery { Symbol = symbol };
            return await getCompanyBySymbolQueryHandler.Handle(query);
        }

        public async Task<IEnumerable<CompanyGetDTO>> RegisterOrUpdateCompanies(IEnumerable<CompanyAddDTO> companyAddDtos)
        {
            var companyDomain = companyAddDtos.ToDomainObjects();
            var addOrUpdateCompanies = new AddOrUpdateCompaniesCommand { companies = companyDomain.ToList() };
            var company = await addOrUpdateCompaniesCommandHandler.Handle(addOrUpdateCompanies);

            return company.ToCompanyGetDTOs();
        }
    }
}
