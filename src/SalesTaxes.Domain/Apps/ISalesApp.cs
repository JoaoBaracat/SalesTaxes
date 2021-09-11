using SalesTaxes.Domain.Entities;
using System.Collections.Generic;

namespace SalesTaxes.Domain.Apps
{
    public interface ISalesApp
    {
        Sale CalculateSale(IEnumerable<SaleItem> saleItems);
    }
}