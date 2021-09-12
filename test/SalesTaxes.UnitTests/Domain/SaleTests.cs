using SalesTaxes.Domain.ValueObjects;
using Xunit;
using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.UnitTests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void ShouldCalculateTotalPriceWithNoTax()
        {
            var saleItemWithNoTax = new SaleItem("Chocolate bar",
                1,
                1.50,
                Origin.National,
                Category.Food);
            saleItemWithNoTax.CalculateSalesPrice();
            saleItemWithNoTax.CalculateTotalPrice();
            var saleWithNoTax = new Sale();
            saleWithNoTax.SaleItems.Add(saleItemWithNoTax);

            saleWithNoTax.CalculateTotalPrice();

            Assert.Equal(1.50, saleWithNoTax.TotalPrice);
        }

        [Fact]
        public void ShouldCalculateTotalPriceWithTaxes()
        {
            var saleItemWithTaxes = new SaleItem("Imported chair",
                2,
                51.50,
                Origin.Imported,
                Category.Others);
            saleItemWithTaxes.CalculateSalesPrice();
            saleItemWithTaxes.CalculateTotalPrice();
            var saleWithTaxes = new Sale();
            saleWithTaxes.SaleItems.Add(saleItemWithTaxes);

            saleWithTaxes.CalculateTotalPrice();

            Assert.Equal(118.46, saleWithTaxes.TotalPrice);
        }

        [Fact]
        public void ShouldCalculateTotalTotalTaxesForImportedOthers()
        {
            var saleItemWithTaxesForImportedOthers = new SaleItem("Imported notebook",
                1,
                1100,
                Origin.Imported,
                Category.Others);
            saleItemWithTaxesForImportedOthers.CalculateSalesPrice();
            saleItemWithTaxesForImportedOthers.CalculateTotalPrice();
            var saleWithTaxes = new Sale();
            saleWithTaxes.SaleItems.Add(saleItemWithTaxesForImportedOthers);

            saleWithTaxes.CalculateTotalTaxes();

            Assert.Equal(165, saleWithTaxes.TotalSalesTaxes);
            Assert.Equal(0, saleWithTaxes.RoundedPrice);
        }

        [Fact]
        public void ShouldCalculateTotalTotalTaxesForOthers()
        {
            var saleItemWithTaxesForOthers = new SaleItem("Notebook",
                1,
                1100,
                Origin.National,
                Category.Others);
            saleItemWithTaxesForOthers.CalculateSalesPrice();
            saleItemWithTaxesForOthers.CalculateTotalPrice();
            var saleWithTaxes = new Sale();
            saleWithTaxes.SaleItems.Add(saleItemWithTaxesForOthers);

            saleWithTaxes.CalculateTotalTaxes();

            Assert.Equal(110, saleWithTaxes.TotalSalesTaxes);
            Assert.Equal(0, saleWithTaxes.RoundedPrice);
        }

        [Fact]
        public void ShouldCalculateTotalTotalTaxesForImported()
        {
            var saleItemWithTaxesForImported = new SaleItem("Imported IT book",
                1,
                120,
                Origin.Imported,
                Category.Books);
            saleItemWithTaxesForImported.CalculateSalesPrice();
            saleItemWithTaxesForImported.CalculateTotalPrice();
            var saleWithTaxes = new Sale();
            saleWithTaxes.SaleItems.Add(saleItemWithTaxesForImported);

            saleWithTaxes.CalculateTotalTaxes();

            Assert.Equal(6, saleWithTaxes.TotalSalesTaxes);
            Assert.Equal(0, saleWithTaxes.RoundedPrice);
        }

        [Fact]
        public void ShouldCalculateTotalTotalTaxesForZeroTax()
        {
            var saleItemWithZeroTaxes = new SaleItem("IT book",
                1,
                60,
                Origin.National,
                Category.Books);
            saleItemWithZeroTaxes.CalculateSalesPrice();
            saleItemWithZeroTaxes.CalculateTotalPrice();
            var saleWithTaxes = new Sale();
            saleWithTaxes.SaleItems.Add(saleItemWithZeroTaxes);

            saleWithTaxes.CalculateTotalTaxes();

            Assert.Equal(0, saleWithTaxes.TotalSalesTaxes);
            Assert.Equal(0, saleWithTaxes.RoundedPrice);
        }
    }
}