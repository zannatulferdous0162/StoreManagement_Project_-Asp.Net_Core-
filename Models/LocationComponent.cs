namespace StoreManagement_Project.Models
{
    public class LocationComponent
    {
        public int? LocationComponentId { get; set; }
        public string? Location { get; set; }
        public int? AisleId { get; set; }
        public Aisle? Aisle { get; set; }
        public int? ZoneId { get; set; }
        public Zone? Zone { get; set; }
        public int? RackId { get; set; }
        public Rack? Rack { get; set; }
        public int? ShelfId { get; set; }
        public Shelf? Shelf { get; set; }
        public int? BinId { get; set; }   
        public Bin? Bin { get; set; }
        public int? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

    }
}
