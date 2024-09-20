using FluentAssertions;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Add;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;

namespace CompanyInventory.Tests.Features.Add
{
    public class AddOrUpdateCompaniesCommandTests
    {
        private readonly ICompanyRepository _repository;
        private readonly IQueryCompanyRepository _queryCompanyRepository;
        private readonly AddOrUpdateCompaniesCommandHandler _handler;

        public AddOrUpdateCompaniesCommandTests()
        {
            _repository = A.Fake<ICompanyRepository>();
            _queryCompanyRepository = A.Fake<IQueryCompanyRepository>();
            _handler = new AddOrUpdateCompaniesCommandHandler(_repository, _queryCompanyRepository);
        }

        [Fact]
        public async Task Handle_TestExistingCompanyAndNewCompany()
        {
            // Arrange
            var existingCompany = Company.Create("Apple Inc.", "AAPL");
            var newCompany = Company.Create("Alphabet Inc.", "GOOGL");

            var command = new AddOrUpdateCompaniesCommand
            {
                companies = new List<Company>
                {
                    Company.Create("Apple Inc. Updated", "AAPL"),
                    newCompany
                }
            };

            A.CallTo(() => _queryCompanyRepository.GetCompaniesBySymbols(A<IEnumerable<string>>.Ignored))
                .Returns(new List<Company> { existingCompany });

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(existingCompany);
            result.Should().Contain(newCompany);

            A.CallTo(() => _repository.Add(newCompany)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_Should_AddAllCompanies_When_NoExistingCompanies()
        {
            // Arrange
            var newCompany1 = Company.Create("Apple Inc.", "AAPL");
            var newCompany2 = Company.Create("Alphabet Inc.", "GOOGL");

            var command = new AddOrUpdateCompaniesCommand
            {
                companies = new List<Company>
                {
                    newCompany1,
                    newCompany2
                }
            };

            A.CallTo(() => _queryCompanyRepository.GetCompaniesBySymbols(A<IEnumerable<string>>.Ignored))
                .Returns(new List<Company>());

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(newCompany1);
            result.Should().Contain(newCompany2);

            A.CallTo(() => _repository.Add(newCompany1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.Add(newCompany2)).MustHaveHappenedOnceExactly();

            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_Should_UpdateAllExistingCompanies_When_AllCompaniesExist()
        {
            // Arrange
            var existingCompany1 = Company.Create("Apple Inc.", "AAPL");
            var existingCompany2 = Company.Create("Alphabet Inc.", "GOOGL");

            var command = new AddOrUpdateCompaniesCommand
            {
                companies = new List<Company>
                {
                    Company.Create("Apple Inc. Updated", "AAPL"),
                    Company.Create("Alphabet Inc. Updated", "GOOGL")
                }
            };

            A.CallTo(() => _queryCompanyRepository.GetCompaniesBySymbols(A<IEnumerable<string>>.Ignored))
                .Returns(new List<Company> { existingCompany1, existingCompany2 });

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(existingCompany1);
            result.Should().Contain(existingCompany2);

            A.CallTo(() => _repository.Add(A<Company>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyList_When_CommandHasNoCompanies()
        {
            // Arrange
            var command = new AddOrUpdateCompaniesCommand
            {
                companies = new List<Company>()
            };

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().BeEmpty();

            A.CallTo(() => _repository.Add(A<Company>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _repository.SaveChanges()).MustNotHaveHappened();
        }
    }
}
