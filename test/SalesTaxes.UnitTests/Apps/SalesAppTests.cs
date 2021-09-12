using Moq;
using SalesTaxes.App.Apps;
using SalesTaxes.Domain.ValueObjects;
using SalesTaxes.Domain.Notifications;
using System.Collections.Generic;
using Xunit;
using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.UnitTests.Apps
{
    public class SalesAppTests
    {
        private Notifier _notifier;
        private SalesApp _salesApp;
        private readonly Mock<INotifier> _notifierMock;

        public SalesAppTests()
        {
            _notifierMock = new Mock<INotifier>();
        }

        [Fact]
        public void ShouldCalculateSaleWithExample1()
        {
            List<SaleItem> listItems = BuildItemsForExample1();
            _salesApp = new SalesApp(_notifierMock.Object);

            var result = _salesApp.CalculateSale(listItems);

            Assert.Equal(42.32, result.TotalPrice);
            Assert.Equal(1.50, result.TotalSalesTaxes);
        }

        private static List<SaleItem> BuildItemsForExample1()
        {
            var saleItemBook = new SaleItem("Book",
                            1,
                            12.49,
                            Origin.National,
                            Category.Books);

            var saleItemCD = new SaleItem("Music CD",
                1,
                14.99,
                Origin.National,
                Category.Others);

            var saleItemChocolate = new SaleItem("Chocolate bar",
                1,
                0.85,
                Origin.National,
                Category.Food);

            var listItems = new List<SaleItem>();
            listItems.Add(saleItemBook);
            listItems.Add(saleItemBook);
            listItems.Add(saleItemCD);
            listItems.Add(saleItemChocolate);
            return listItems;
        }

        [Fact]
        public void ShouldCalculateSaleWithExample2()
        {
            List<SaleItem> listItems = BuildItemsForExample2();
            _salesApp = new SalesApp(_notifierMock.Object);

            var result = _salesApp.CalculateSale(listItems);

            Assert.Equal(65.15, result.TotalPrice);
            Assert.Equal(7.65, result.TotalSalesTaxes);
            Assert.Equal(0.02, result.RoundedPrice);
        }

        private static List<SaleItem> BuildItemsForExample2()
        {
            var saleItemImportedChocolate = new SaleItem("Imported box of chocolates",
                            1,
                            10.00,
                            Origin.Imported,
                            Category.Food);

            var saleItemImportedPerfume = new SaleItem("Imported bottle of perfume",
                1,
                47.50,
                Origin.Imported,
                Category.Cosmetics);

            var listItems = new List<SaleItem>();
            listItems.Add(saleItemImportedChocolate);
            listItems.Add(saleItemImportedPerfume);
            return listItems;
        }

        [Fact]
        public void ShouldCalculateSaleWithExample3()
        {
            List<SaleItem> listItems = BuildItemsForExample3();
            _salesApp = new SalesApp(_notifierMock.Object);

            var result = _salesApp.CalculateSale(listItems);

            Assert.Equal(86.48, result.TotalPrice);
            Assert.Equal(7.25, result.TotalSalesTaxes);
        }

        private static List<SaleItem> BuildItemsForExample3()
        {
            var saleItemImportedPerfume = new SaleItem("Imported bottle of perfume",
                            1,
                            27.99,
                            Origin.Imported,
                            Category.Cosmetics);

            var saleItemPerfume = new SaleItem("Bottle of perfume",
                1,
                18.99,
                Origin.National,
                Category.Cosmetics);

            var saleItemPills = new SaleItem("Packet of headache pills",
                1,
                9.75,
                Origin.National,
                Category.MedicalProducts);

            var saleImportedChocolate = new SaleItem("Imported box of chocolates",
                1,
                11.25,
                Origin.Imported,
                Category.Food);

            var listItems = new List<SaleItem>();
            listItems.Add(saleItemImportedPerfume);
            listItems.Add(saleItemPerfume);
            listItems.Add(saleItemPills);
            listItems.Add(saleImportedChocolate);
            listItems.Add(saleImportedChocolate);
            return listItems;
        }

        [Fact]
        public void ShouldNotCalculateSaleWithNoPriceNotification()
        {
            var saleItemWithNoPrice = new SaleItem("Book",
                1,
                0,
                Origin.National,
                Category.Books);
            var listItems = new List<SaleItem>();
            listItems.Add(saleItemWithNoPrice);
            _notifier = new Notifier();
            _salesApp = new SalesApp(_notifier);

            var result = _salesApp.CalculateSale(listItems);
            var notifications = _notifier.GetNotifications();

            Assert.True(_notifier.HasNotifications());
            Assert.Equal(2, notifications.Count);
            Assert.Equal("The Unit Price must be supplied", notifications[0].Message);
            Assert.Equal("The Unit Price must be greater than 0", notifications[1].Message);
        }

        [Fact]
        public void ShouldNotCalculateSaleWithNoPrice()
        {
            var saleItemWithNoPrice = new SaleItem("Book",
                1,
                0,
                Origin.National,
                Category.Books);
            var listItems = new List<SaleItem>();
            listItems.Add(saleItemWithNoPrice);
            _salesApp = new SalesApp(_notifierMock.Object);

            var result = _salesApp.CalculateSale(listItems);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldNotCalculateSaleWithNoQuantity()
        {
            var saleItemWithNoQuantity = new SaleItem("Book",
                0,
                14.99,
                Origin.Imported,
                Category.Books);
            var listItems = new List<SaleItem>();
            listItems.Add(saleItemWithNoQuantity);
            _salesApp = new SalesApp(_notifierMock.Object);

            var result = _salesApp.CalculateSale(listItems);

            Assert.Null(result);
        }
    }
}