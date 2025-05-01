namespace StoreManagement_Project.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int UnitId { get; set; } // For unit consistency
        public Unit? Unit { get; set; }

        public decimal Quantity { get; set; }

        public int? LocationComponentId { get; set; }
        public LocationComponent? LocationComponent { get; set; }
    }
}
