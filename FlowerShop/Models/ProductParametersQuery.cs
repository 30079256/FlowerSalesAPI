namespace FlowerSales.API.Models
{
    public class ProductParametersQuery : QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string StoreLocation { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
