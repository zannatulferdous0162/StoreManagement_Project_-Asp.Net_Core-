using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreManagement_Project.Models
{
    public class LocationComponentViewModel
    {
        public int? AisleId { get; set; }
        public int? ZoneId { get; set; }
        public int? RackId { get; set; }
        public int? ShelfId { get; set; }
        public int? BinId { get; set; }
        public int? WarehouseId { get; set; }

        public SelectList? Aisles { get; set; }
        public SelectList? Zones { get; set; }
        public SelectList? Racks { get; set; }
        public SelectList? Shelves { get; set; }
        public SelectList? Bins { get; set; }
        public SelectList? Warehouses { get; set; }
    }
}
