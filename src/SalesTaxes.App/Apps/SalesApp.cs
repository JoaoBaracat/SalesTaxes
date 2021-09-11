using SalesTaxes.Domain.Apps;
using SalesTaxes.Domain.Entities;
using SalesTaxes.Domain.Entities.Validation;
using SalesTaxes.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes.App.Apps
{
    public class SalesApp : AppBase, ISalesApp
    {
        public SalesApp(INotifier notifier) : base(notifier)
        {
        }

        public Sale CalculateSale(IEnumerable<SaleItem> saleItems)
        {
            var sale = new Sale();
            var groupedSaleItems = saleItems
                .GroupBy(x => (x.Description, x.Category, x.Origin));

            foreach (var group in groupedSaleItems)
            {
                foreach (var item in group)
                {
                    if (!Validate(new SaleItemValidation(), item))
                    {
                        return null;
                    }
                }
                var groupedItem = new SaleItem(
                    group.FirstOrDefault().Description,
                    group.Sum(g => g.Quantity),
                    group.Sum(g => g.UnitPrice) / group.Sum(g => g.Quantity),
                    group.FirstOrDefault().Origin,
                    group.FirstOrDefault().Category
                 );

                groupedItem.CalculateSalesPrice();
                groupedItem.CalculateTotalPrice();
                sale.SaleItems.Add(groupedItem);
            }

            sale.CalculateTaxTotals();
            sale.CalculateTotalPrice();

            if (sale.RoundedPrice > 0)
            {
                saleItems.Where(x => x.SalePrice > x.UnitPrice).LastOrDefault().FixTaxPrice(sale.RoundedPrice);
            }

            return sale;
        }
    }
}