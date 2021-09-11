using System;
using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.Domain.Entities
{
    public class SaleItem : Entity
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SalePrice { get; private set; }
        public double TotalPrice { get; private set; }
        public Origin Origin { get; set; }
        public Category Category { get; set; }

        public SaleItem(string description, int quantity, double unitPrice, Origin origin, Category category)
        {
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Origin = origin;
            Category = category;
        }

        public void CalculateSalesPrice()
        {
            SalePrice = UnitPrice;

            if (Category != Category.Books && Category != Category.Food && Category != Category.MedicalProducts)
            {
                SalePrice += UnitPrice * 0.10;
            }

            if (Origin == Origin.Imported)
            {
                SalePrice += UnitPrice * 0.05;
            }

            SalePrice = Math.Round(SalePrice, 2);
        }

        public void FixTaxPrice(double roundedPrice)
        {
            SalePrice += roundedPrice / Quantity;
            SalePrice = Math.Round(SalePrice, 2);
            CalculateTotalPrice();
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = Math.Round(SalePrice * Quantity, 2);
        }
    }
}