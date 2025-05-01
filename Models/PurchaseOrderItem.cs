namespace StoreManagement_Project.Models
{
    public class PurchaseOrderItem
    {
        public int PurchaseOrderItemId { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        //public int UOMId { get; set; }
        public int UnitId { get; set; }
        public virtual Unit? Unit { get; set; }
        //public UOM? UOM { get; set; }
        public int PurchaseOrderId { get; set; }  // Foreign Key
        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}
