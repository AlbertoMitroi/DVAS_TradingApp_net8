using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace InternshipTradingApp.CompanyInventory.Domain.CompanyHistory
{
    public class CompanyHistoryEntry
    {
        public ulong Id { get;  set; }
        public string CompanySymbol { get; set; } = string.Empty;
        public decimal Price { get;  set; }
        public decimal ReferencePrice { get;  set; }
        public decimal OpeningPrice { get;  set; }
        public decimal ClosingPrice { get;  set; }
        public decimal EPS { get;  set; }
        public decimal PER { get;  set; }
        public decimal DayVariation { get;  set; }
        public DateOnly Date { get;  set; }
        public Company Company { get; set; }

        public decimal Volume { get; set; }
        public CompanyHistoryEntry() { }

        public static CompanyHistoryEntry Create(string companySymbol,
                                                 decimal price,
                                                 decimal openingPrice,
                                                 decimal closingPrice,
                                                 decimal referencePrice,
                                                 decimal eps,
                                                 decimal volume)
        {
            if (string.IsNullOrEmpty(companySymbol) || companySymbol.Length > 10)
                throw new ArgumentException("Symbol cannot be null, empty, or exceed 10 characters.", nameof(companySymbol));

            ValidatePrice(price);
            ValidatePrice(openingPrice);
            ValidatePrice(closingPrice);
            ValidateReferencePrice(referencePrice);
            ValidateEps(eps);

            return new CompanyHistoryEntry
            {
                CompanySymbol = companySymbol,
                Price = price,
                OpeningPrice = openingPrice,
                ClosingPrice = closingPrice,
                ReferencePrice = referencePrice,
                EPS = eps,
                PER = eps > 0 ? Math.Round(price / eps, 2) : 0,
                DayVariation = referencePrice > 0 ? Math.Round((price - referencePrice) / referencePrice * 100, 2) : 0,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Volume = volume
            };
        }

        public void UpdateTradingData(CompanyHistoryEntry newCompanyData)
        {
            if (newCompanyData == null)
                throw new ArgumentNullException(nameof(newCompanyData));

            ValidateTradingData(newCompanyData);

            Price = newCompanyData.Price;
            OpeningPrice = newCompanyData.OpeningPrice;
            ClosingPrice = newCompanyData.ClosingPrice;
            ReferencePrice = newCompanyData.ReferencePrice;
            EPS = newCompanyData.EPS;
            PER = newCompanyData.EPS > 0 ? Math.Round(newCompanyData.Price / newCompanyData.EPS, 2) : 0;
            DayVariation = newCompanyData.ReferencePrice > 0 ? Math.Round((newCompanyData.Price - newCompanyData.ReferencePrice) / newCompanyData.ReferencePrice * 100, 2) : 0;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Volume = newCompanyData.Volume;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");

            if (newPrice < ReferencePrice * 0.1m)
                throw new ArgumentException("New price is too low compared to the reference price.", nameof(newPrice));

            Price = newPrice;
        }

        private static void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }

        private static void ValidateReferencePrice(decimal referencePrice)
        {
            if (referencePrice < 0)
                throw new ArgumentOutOfRangeException(nameof(referencePrice), "Reference price must be greater than zero.");
        }

        private static void ValidateEps(decimal eps)
        {
            if (eps < 0)
                throw new ArgumentOutOfRangeException(nameof(eps), "EPS cannot be negative.");
        }

        private void ValidateTradingData(CompanyHistoryEntry data)
        {
            ValidatePrice(data.Price);
            ValidatePrice(data.OpeningPrice);
            ValidatePrice(data.ClosingPrice);
            ValidateReferencePrice(data.ReferencePrice);
            ValidateEps(data.EPS);
        }
    }
}
