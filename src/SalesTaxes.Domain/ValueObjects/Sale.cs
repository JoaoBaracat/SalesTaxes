using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes.Domain.ValueObjects
{
    public class Sale
    {
        public double TotalSalesTaxes { get; private set; }
        public double TotalPrice { get; private set; }
        public double RoundedPrice { get; private set; }
        public ICollection<SaleItem> SaleItems { get; set; }

        public Sale()
        {
            SaleItems = new List<SaleItem>();
        }

        public void CalculateTotalTaxes()
        {
            var originalTaxPrice = Math.Round(SaleItems.Sum(x => (x.SalePrice - x.UnitPrice) * x.Quantity), 2);
            TotalSalesTaxes = RoundingUpTaxes(originalTaxPrice);
            RoundedPrice = Math.Round(TotalSalesTaxes - originalTaxPrice, 2);
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = Math.Round(SaleItems.Sum(x => x.TotalPrice) + RoundedPrice, 2);
        }

        public double RoundingUpTaxes(double totalTaxes)
        {
            return Math.Round(Math.Ceiling(totalTaxes * 20) / 20, 2);
        }
    }
}