﻿using InternshipTradingApp.CompanyInventory.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;


namespace InternshipTradingApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInventoryController : ControllerBase
    {
        private readonly ICompanyInventoryService companyInventoryService;
        private readonly ICompanyHistoryInventoryService companyHistoryInventoryService;

        public CompanyInventoryController(ICompanyInventoryService companyInventoryService, ICompanyHistoryInventoryService companyHistoryInventoryService)
        {
            this.companyInventoryService = companyInventoryService;
            this.companyHistoryInventoryService = companyHistoryInventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                var company = await companyHistoryInventoryService.GetCompanyWithHistoryDataAsync(symbol);
                if (company != null)
                {
                    return Ok(company);
                }
                return NotFound(symbol);
            }

            var allCompanies = await companyInventoryService.GetAllCompanies();
            return Ok(allCompanies);
        }

        [HttpGet("topXCompaniesByParameter")]
        public async Task<IEnumerable<CompanyWithHistoryGetDTO>> GetTopXCompanies([FromQuery] int? x, string? value)
        {
            return await companyInventoryService.GetTopXCompanies(x, value);
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