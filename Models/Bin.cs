namespace StoreManagement_Project.Models
{
    public class Aisle
    {
        public int AisleId { get; set; }
        public string? AisleName { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
