namespace StoreManagement_Project.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        //public int UOMId { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        //public UOM? UOM { get; set; }
        public int PackSizeId { get; set; }
        public PackSize? PackSize { get; set; }



        public ICollection<PurchaseOrderItem>? PurchaseOrderItems { get; set; }

    }
}
