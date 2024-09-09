using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    public class GetTopXCompaniesQueryHandler
    {
        private readonly IQueryCompanyRepository _queryCompanyRepository;

        public GetTopXCompaniesQueryHandler(IQueryCompanyRepository queryCompanyRepository)
        {
            _queryCompanyRepository = queryCompanyRepository
                ?? throw new ArgumentNullException(nameof(queryCompanyRepository));
        }

        public async Task<IEnumerable<CompanyWithHistoryGetDTO>> Handle(GetTopXCompaniesQuery? query)
        {
            var x = query?.X ?? 10; 
            var value = query?.Value ?? "volume"; 

            var allCompanies = await _queryCompanyRepository.GetTopXCompanies(x, value);

            
            if (allCompanies == null || !allCompanies.Any())
            {
                throw new NullReferenceException("No companies were returned from the repository.");
            }

            return allCompanies.Select(company => company.ToCompanyWithHistoryGetDTO());
        }

    }


}
