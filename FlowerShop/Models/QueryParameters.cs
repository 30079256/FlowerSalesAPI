namespace FlowerSales.API.Models
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 50;

        public int Page { get; set; } = 1;
        public int Size
        {
            get => _size;
            set => _size = Math.Min(_maxSize, value);
        }

        public string SortBy { get; set; } = "_id";  
        private string sortOrder = "asc";

        public string SortOrder
        {
            get => sortOrder;
            set => sortOrder = (value == "desc" ? "desc" : "asc");  
        }
    }
}