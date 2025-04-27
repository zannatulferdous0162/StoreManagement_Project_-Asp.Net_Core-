namespace StoreManagement_Project.Models
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public string? ShelfName { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
