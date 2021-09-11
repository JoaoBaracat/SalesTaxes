using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.API.ViewModel
{
    public class SaleItemViewModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Origin Origin { get; set; }
        public Category Category { get; set; }
    }
}