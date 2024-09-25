using FakeItEasy;
using FluentAssertions;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Domain.MarketIndex;
using InternshipTradingApp.CompanyInventory.Infrastructure;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using InternshipTradingApp.CompanyInventory.Infrastructure.MarketIndexDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInventory.Tests.Infrastructure
{
    public class MarketIndexServiceTests
    {
        private readonly IMarketIndexRepository _marketIndexRepository;
        private readonly IQueryCompanyRepository _queryCompanyRepository;
        private readonly MarketIndexService _marketIndexService;

        public MarketIndexServiceTests() 
        {
            _marketIndexRepository = A.Fake<IMarketIndexRepository>();

            _queryCompanyRepository = A.Fake<IQueryCompanyRepository>();

            _marketIndexService = new MarketIndexService(_marketIndexRepository, _queryCompanyRepository);
        }

        [Fact]
        public async Task MarketIndexService_CalculateAndSaveMarketIndex_CalculatesProperlyAndCallsSave() 
        {
            //Arrange
            var companies = new List<Company>
            {
                new Company
                {
                    Name = "Company A",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 100 },
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Price = 80 }
                    }
                },
                new Company
                {
                    Name = "Company B",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 200 },
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Price = 150 }
                    }
                },
                new Company
                {
                    Name = "Company C",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 300 },
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Price = 250 }
                    }
                }
            };

            
            A.CallTo(() => _queryCompanyRepository.GetAllCompanies()).Returns(companies);

            //Act
            var result = await _marketIndexService.CalculateAndSaveMarketIndex();

            var expectedMarketIndex = (100 + 200 + 300) / 3m;

            //Assert
            A.CallTo(() => _marketIndexRepository
                .SaveMarketIndexHistoryAsync(A<decimal>.That.IsGreaterThan(0)))
                .MustHaveHappenedOnceExactly();

            result.Should().Be(expectedMarketIndex);
        }

        [Fact]
        public async Task MarketIndexService_GetMarketIndexHistory_ReturnsHistoryAndChecksForValidEntries() 
        {
            //Arrange
            var expectedHistory = new List<MarketIndexHistory>
            { new MarketIndexHistory { Date = DateOnly.FromDateTime(DateTime.Now), Value = 13},
              new MarketIndexHistory { Date = DateOnly.FromDateTime(DateTime.Now), Value = 20}
            };

            A.CallTo(() => _marketIndexRepository.GetMarketIndexHistoriesAsync())
                .Returns(expectedHistory);

            //Act
            var history = await _marketIndexService.GetMarketIndexHistory();

            //Assert
            A.CallTo(() => _marketIndexRepository.GetMarketIndexHistoriesAsync())
                .MustHaveHappenedOnceExactly();


            history.Should().NotBeEmpty("there should be at least one history entry returned");   
            history.Should().OnlyContain(h => h.Value > 0, "every history entry should have a value greater than 0")
                   .And.OnlyContain(h => h.Date > DateOnly.MinValue, "every history entry should have a valid date");
        }


        [Fact]
        public async Task MarketIndexService_SavesMarketIndexWithSameValueButDifferentDate()
        {
            // Arrange
            var existingHistory = new List<MarketIndexHistory>
            {
                new MarketIndexHistory { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Value = 200 }
            };

            A.CallTo(() => _marketIndexRepository.GetMarketIndexHistoriesAsync()).Returns(existingHistory);

            var companies = new List<Company>
            {
                new Company
                {
                    Name = "Company A",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 100 }
                    }
            },
                new Company
                {
                    Name = "Company B",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 300 }
                    }
                }
            };

            A.CallTo(() => _queryCompanyRepository.GetAllCompanies()).Returns(companies);

            // Act: Calculate and save the market index (same value, different date)
            var result = await _marketIndexService.CalculateAndSaveMarketIndex();

            
            var expectedMarketIndex = (100 + 300) / 2m;

            // Assert
            A.CallTo(() => _marketIndexRepository.SaveMarketIndexHistoryAsync(expectedMarketIndex))
                .MustHaveHappenedOnceExactly();

            result.Should().Be(expectedMarketIndex, "the market index should be calculated and saved because the date is different.");
        }

        //Handling Companies without History Entries
        [Fact]
        public async Task MarketIndexService_IgnoresCompaniesWithoutHistoryEntries()
        {
            
            var companies = new List<Company>
            {
                new Company
                {
                    Name = "Petrom",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 100 }
                    }
                },
                new Company
                {
                    Name = "BCR",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>() // No entries
                },
                new Company
                {
                    Name = "Aquila",
                    CompanyHistoryEntries = new List<CompanyHistoryEntry>
                    {
                        new CompanyHistoryEntry { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Price = 300 }
                    }
                }
            };

            A.CallTo(() => _queryCompanyRepository.GetAllCompanies()).Returns(companies);

            // Act
            var result = await _marketIndexService.CalculateAndSaveMarketIndex();

            // The expected market index should ignore Company B with no history entries
            var expectedMarketIndex = (100 + 300) / 2m;

            // Assert
            result.Should().Be(expectedMarketIndex, "the market index should only consider companies with valid history entries.");
        }

        

        //Behavior When No Companies are Returned
        [Fact]
        public async Task MarketIndexService_ReturnsZeroWhenNoCompanies()
        {
            // Arrange:
            A.CallTo(() => _queryCompanyRepository.GetAllCompanies()).Returns(new List<Company>());

            // Act
            var result = await _marketIndexService.CalculateAndSaveMarketIndex();

            // Assert
            result.Should().Be(0, "the market index should be zero if there are no companies.");
        }

    }

}
