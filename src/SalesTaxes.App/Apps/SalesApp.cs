using SalesTaxes.Domain.Apps;
using SalesTaxes.Domain.ValueObjects;
using SalesTaxes.Domain.ValueObjects.Validation;
using SalesTaxes.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes.App.Apps
{
    public class SalesApp : AppBase, ISalesApp
    {
        private readonly INotifier _notifier;
        private Sale _sale;

        public SalesApp(INotifier notifier) : base(notifier)
        {
            _notifier = notifier;
        }

        public Sale CalculateSale(IEnumerable<SaleItem> saleItems)
        {
            _sale = new Sale();
            var groupedSaleItems = saleItems
                .GroupBy(x => (x.Description, x.Category, x.Origin));

            foreach (var group in groupedSaleItems)
            {
                var groupedItem = new SaleItem(
                    group.FirstOrDefault().Description,
                    group.Sum(g => g.Quantity),
                    group.Sum(g => g.UnitPrice) / group.Sum(g => g.Quantity),
                    group.FirstOrDefault().Origin,
                    group.FirstOrDefault().Category
                 );

                if (!Validate(new SaleItemValidation(), groupedItem))
                {
                    return null;
                }

                groupedItem.CalculateSalesPrice();
                groupedItem.CalculateTotalPrice();
                _sale.SaleItems.Add(groupedItem);
            }

            _sale.CalculateTotalTaxes();
            _sale.CalculateTotalPrice();

            FixRoundedPrice();

            return _sale;
        }

        private void FixRoundedPrice()
        {
            if (_sale.RoundedPrice > 0)
            {
                _sale.SaleItems.Where(x => x.SalePrice > x.UnitPrice).LastOrDefault().FixTaxPrice(_sale.RoundedPrice);
            }
        }
    }
}