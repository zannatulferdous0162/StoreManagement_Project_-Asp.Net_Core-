namespace StoreManagement_Project.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; } 
        public string? Address { get; set; }
        public ICollection<LocationComponent>? LocationComponents { get; set; } = new List<LocationComponent>();
    }
}
