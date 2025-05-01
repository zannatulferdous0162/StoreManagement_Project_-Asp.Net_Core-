namespace StoreManagement_Project.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        public string? SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
