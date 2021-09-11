using System.Collections.Generic;

namespace SalesTaxes.API.ViewModel
{
    public class SaleResultViewModel
    {
        public double TotalSalesTaxes { get; private set; }
        public double TotalPrice { get; private set; }
        public ICollection<SaleItemResultViewModel> SaleItems { get; set; }
    }
}