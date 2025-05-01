namespace StoreManagement_Project.Models
{
    public class Zone
    {
        public int ZoneId { get; set; }
        public string? ZoneName { get; set; } 
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
