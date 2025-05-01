namespace StoreManagement_Project.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactNo { get; set; }
        public string? EmailAddress { get; set; }



        public  virtual ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

    }
}
