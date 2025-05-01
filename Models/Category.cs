namespace StoreManagement_Project.Models
{
    public class Category
    {      
       public int CategoryId { get; set; }
       public string? CategoryName { get; set; }
       public ICollection<SubCategory>? SubCategories { get; set; }
    }
}

