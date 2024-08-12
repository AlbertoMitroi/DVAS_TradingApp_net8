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

        public CompanyInventoryController(ICompanyInventoryService companyInventoryService)
        {
            this.companyInventoryService = companyInventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyGetDTO>> GetAll()
        {
            return await companyInventoryService.GetAllCompanies();
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            var company = await companyInventoryService.GetCompanyBySymbol(symbol);
            if (company != null)
            {
                return Ok(company);
            }
            return NotFound(symbol);
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
