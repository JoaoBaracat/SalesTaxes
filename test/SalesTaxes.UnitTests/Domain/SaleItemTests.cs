using SalesTaxes.Domain.ValueObjects;
using Xunit;
using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.UnitTests.Domain
{
    public class SaleItemTests
    {
        [Fact]
        public void ShouldCalculatePriceWithNoTax()
        {
            var saleItemWithNoTax = new SaleItem("Chocolate bar",
                1,
                1.50,
                Origin.National,
                Category.Food);

            saleItemWithNoTax.CalculateSalesPrice();

            Assert.Equal(saleItemWithNoTax.UnitPrice, saleItemWithNoTax.SalePrice);
        }

        [Fact]
        public void ShouldCalculatePriceWithTaxes()
        {
            var saleItemWithNoTax = new SaleItem("Imported notebook",
                1,
                1100,
                Origin.Imported,
                Category.Others);

            saleItemWithNoTax.CalculateSalesPrice();

            Assert.Equal(1265, saleItemWithNoTax.SalePrice);
        }
    }
}