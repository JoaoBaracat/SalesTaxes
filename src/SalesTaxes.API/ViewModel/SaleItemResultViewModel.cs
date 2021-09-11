﻿namespace SalesTaxes.API.ViewModel
{
    public class SaleItemResultViewModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; private set; }
    }
}