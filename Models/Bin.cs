namespace StoreManagement_Project.Models
{
    public class Bin
    {
        public int BinId { get; set; }
        public string? BinName { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
