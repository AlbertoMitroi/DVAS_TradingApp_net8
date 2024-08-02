using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;


namespace InternshipTradingApp.CompanyInventory
{
    internal class CompanyInventoryService:ICompanyInventoryService
    {
        private readonly GetCompanyBySymbolQueryHandler getCompanyBySymbolHandler; 
        public CompanyInventoryService(
                                    GetCompanyBySymbolQueryHandler getCompanyQueryHandler) 
        {
            getCompanyBySymbolHandler = getCompanyQueryHandler;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompanies()
        {
            //var queryHandler = new GetAllCompaniesQuery();
            return new List<CompanyDTO>(); 
        }

        public async Task<CompanyDTO?> GetCompanyBySymbol(string symbol)
        {
            var getCompanyQuery = new GetCompanyBySymbolQuery { Symbol = symbol };
            return await getCompanyBySymbolHandler.Handle(getCompanyQuery);
        }

        public async Task RegisterOrUpdateCompanies(IEnumerable<CompanyDTO> companies)
        {
            throw new NotImplementedException();
        }
    }
}
