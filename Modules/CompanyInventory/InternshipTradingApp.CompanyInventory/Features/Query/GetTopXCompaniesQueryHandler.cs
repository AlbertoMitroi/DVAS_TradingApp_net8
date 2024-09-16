using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

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
            var value = query?.Value ?? "price";
            var orderToggle = query?.OrderToggle ?? "desc";

            var allCompanies = await _queryCompanyRepository.GetTopXCompanies(x, value, orderToggle);


            if (allCompanies == null || !allCompanies.Any())
            {
                throw new NullReferenceException("No companies were returned from the repository.");
            }

            return allCompanies.Select(company => company.ToCompanyWithHistoryGetDTO());
        }

    }


}
