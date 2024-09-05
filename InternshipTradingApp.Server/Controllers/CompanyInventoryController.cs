using Microsoft.AspNetCore.Mvc;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using InternshipTradingApp.CompanyInventory.Domain;


namespace InternshipTradingApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInventoryController : ControllerBase
    {
        private readonly ICompanyInventoryService companyInventoryService;
        private readonly ICompanyHistoryInventoryService companyHistoryInventoryService;

        public CompanyInventoryController(ICompanyInventoryService companyInventoryService,ICompanyHistoryInventoryService companyHistoryInventoryService)
        {
            this.companyInventoryService = companyInventoryService;
            this.companyHistoryInventoryService = companyHistoryInventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyGetDTO>> GetAll()
        {
            return await companyInventoryService.GetAllCompanies();
        }
       
        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            var company = await companyHistoryInventoryService.GetCompanyWithHistoryDataAsync(symbol);
            if (company != null)
            {
                return Ok(company);
            }
            return NotFound(symbol);
        }
        [HttpPost("history")]
        public async Task<IEnumerable<CompanyHistoryGetDTO>> Post([FromBody] IEnumerable<CompanyHistoryAddDTO> companyHistoryDtos)
        {

            if (companyHistoryDtos == null)
            {
                throw new ArgumentNullException(nameof(companyHistoryDtos));
            }

            return await this.companyHistoryInventoryService.RegisterCompaniesHistory(companyHistoryDtos);
        }
        [HttpPost("external-data")]
        public async Task<IEnumerable<CompanyGetDTO>> Post([FromBody] IEnumerable<CompanyAddDTO> companyDtos)
        {

            if (companyDtos == null)
            {
                throw new ArgumentNullException(nameof(companyDtos));
            }

            return await this.companyInventoryService.RegisterOrUpdateCompanies(companyDtos);
        }
       
    }
}
