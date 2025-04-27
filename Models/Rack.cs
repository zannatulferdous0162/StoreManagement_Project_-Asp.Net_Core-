namespace StoreManagement_Project.Models
{
    public class Rack
    {
        public int RackId { get; set; }
        public string? RackName { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
