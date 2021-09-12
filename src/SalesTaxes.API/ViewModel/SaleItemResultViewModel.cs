namespace SalesTaxes.API.ViewModel
{
    public class SaleItemResultViewModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; private set; }
    }
}