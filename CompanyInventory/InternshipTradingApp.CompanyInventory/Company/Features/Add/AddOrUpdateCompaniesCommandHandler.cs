using InternshipTradingApp.CompanyInventory.Company.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Company.Features.Add
{
    internal class AddOrUpdateCompaniesCommandHandler
    {
        private readonly ICompanyRepository companyRepository;
        public AddOrUpdateCompaniesCommandHandler(ICompanyRepository repository) 
        { 
            companyRepository= repository;
        }

        public async Task Handle(AddOrUpdateCompaniesCommand command)
        {
                     
            Dictionary<int, CompanyDTO> companiesMap = new Dictionary<int, CompanyDTO>();
            foreach (var company in command.companies) 
            {
                companiesMap[company.Id] = company;
            }
            var companiesToUpdate = await companyRepository
                                        .GetExistingCompanies(companiesMap.Keys);

                        
            foreach ( var existingCompany in companiesToUpdate ) 
            {
                var newData = companiesMap[existingCompany.Id]
                                .ToDomainObject();

                companiesMap.Remove(existingCompany.Id);
                existingCompany.UpdateTradingData(newData);               
            }

            foreach (var newCompanyEntry in companiesMap)
            { 
                await companyRepository.Add(newCompanyEntry.Value
                                                            .ToDomainObject());
            }

            await companyRepository.SaveChanges();          

        }
    }
}
