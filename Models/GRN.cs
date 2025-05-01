namespace StoreManagement_Project.Models
{
    public class GRN
    {
        public int GRNId { get; set; }
        public string? GRNNumber { get; set; } = string.Empty; // e.g., GRN-PO-2025-01/01
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; } //Navigation Property
        public int? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public int? LocationComponentId { get; set; } // jodi per item location lage
        public LocationComponent? LocationComponent { get; set; }
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;
        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? ReceivedBy { get; set; }
        // Flattened GRN items inline
        public ICollection<GRNItem>? GRNItems { get; set; } = new List<GRNItem>();
    }
}
