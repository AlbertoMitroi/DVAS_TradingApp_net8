using InternshipTradingApp.CompanyInventory;
using InternshipTradingApp.CompanyInventory.Company.Features.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IEnumerable<CompanyDTO>> Get()
        {
            return await companyInventoryService.GetAllCompanies();
        }

        // GET api/<CompanyInventoryController>/5
        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            var company = await companyInventoryService.GetCompanyBySymbol(symbol);
            if (company!=null) 
            {
                return Ok(company);
            }
            return NotFound(symbol);
        }

        // POST api/<CompanyInventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompanyInventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompanyInventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
