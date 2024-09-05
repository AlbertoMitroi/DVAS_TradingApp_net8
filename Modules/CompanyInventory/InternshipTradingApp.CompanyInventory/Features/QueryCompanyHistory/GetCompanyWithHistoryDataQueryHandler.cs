﻿using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory
{
    
    public class GetCompanyWithHistoryDataQueryHandler(IQueryCompanyRepository queryCompanyRepository,IQueryCompanyHistoryRepository queryCompanyHistoryRepository)
    {
        public async Task<CompanyWithHistoryGetDTO> Handle(GetCompanyWithHistoryDataQuery getCompanyWithHistoryDataQuery)
        {

            var companies = await queryCompanyRepository.GetAllCompaniesHistory(getCompanyWithHistoryDataQuery.CompanySymbol);
            var company = companies.FirstOrDefault();
            if (company == null)
            {
                throw new Exception($"Company with symbol {getCompanyWithHistoryDataQuery.CompanySymbol} not found.");
            }
            //Transform each companies in companies using a mapper to the compose dto.
            var result = company.ToCompanyWithHistoryGetDTO();

            return result;
        }
    }
}