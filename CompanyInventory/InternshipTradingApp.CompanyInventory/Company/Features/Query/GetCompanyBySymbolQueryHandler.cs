using InternshipTradingApp.CompanyInventory.Company.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Company.Features.Query
{
    internal class GetCompanyBySymbolQueryHandler
    {
        private readonly IQueryCompanyRepository companyQueryRepository;
        public GetCompanyBySymbolQueryHandler(IQueryCompanyRepository companyRepository)
        {
            companyQueryRepository = companyRepository;
        }
        //return CompanyDTO for now. this will be changed later to GetCompanyQueryResult
        public async Task<CompanyDTO?> Handle(GetCompanyBySymbolQuery query)
        {
            var companySymbol = query.Symbol;                
            var company = await companyQueryRepository.GetCompanyBySymbol(companySymbol);
            if (company != default) 
                return company.ToDTO();

            return default;
        }
    }
}
