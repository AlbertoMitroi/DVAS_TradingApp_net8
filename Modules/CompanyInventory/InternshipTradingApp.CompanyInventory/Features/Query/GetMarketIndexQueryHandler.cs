using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    public class GetMarketIndexQueryHandler
    {
        private readonly IQueryCompanyRepository _queryCompanyRepository;
        public GetMarketIndexQueryHandler(IQueryCompanyRepository queryCompanyRepository)
        {
           _queryCompanyRepository = queryCompanyRepository
                ?? throw new ArgumentNullException(nameof(queryCompanyRepository));
        }

        public async Task<decimal> Handle()
        {
            var marketIndex = await _queryCompanyRepository.GetMarketIndex();

            return marketIndex;
        }
    }
}
