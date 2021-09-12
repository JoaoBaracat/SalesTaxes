using Newtonsoft.Json;
using SalesTaxes.API;
using SalesTaxes.API.ViewModel;
using SalesTaxes.IntegrationTests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static SalesTaxes.Domain.Enums.CategoryEnum;
using static SalesTaxes.Domain.Enums.OriginEnum;

namespace SalesTaxes.IntegrationTests
{
    public class SalesIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public SalesIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldCalculateSalesTaxesReturnOK()
        {
            var client = _factory.GetAnonymousClient();
            var saleItemsViewModel = new SaleItemViewModel
            {
                Description = "Chocolate box",
                Quantity = 1,
                UnitPrice = 3.99,
                Origin = Origin.National,
                Category = Category.Food
            };
            StringContent stringContent = ConvertSalesItemsVMToStringContent(saleItemsViewModel);

            HttpResponseMessage response = await client.PutAsync("/api/v1.0/sales", stringContent);
            response.EnsureSuccessStatusCode();

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldCalculateSalesTaxesReturnBadRequest()
        {
            var client = _factory.GetAnonymousClient();
            var saleItemsViewModel = new SaleItemViewModel
            {
                Description = "Chocolate box",
                Quantity = 0,
                UnitPrice = 3.99,
                Origin = Origin.National,
                Category = Category.Food
            };
            StringContent stringContent = ConvertSalesItemsVMToStringContent(saleItemsViewModel);

            HttpResponseMessage response = await client.PutAsync("/api/v1.0/sales", stringContent);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        private static StringContent ConvertSalesItemsVMToStringContent(SaleItemViewModel saleItemsViewModel)
        {
            var listItems = new List<SaleItemViewModel>();
            listItems.Add(saleItemsViewModel);
            var json = JsonConvert.SerializeObject(listItems);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return stringContent;
        }
    }
}