using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreManagement_Project.Models
{
    public class ItemViewModel
    {
        // Display Names (for Create)
        //public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        //public int UOMId { get; set; }
        public int UnitId { get; set; }

        public int PackSizeId { get; set; }
        public string? ItemName { get; set; }



        //// Display Names (for Index)
        //public string CategoryName { get; set; }
        //public string SubCategoryName { get; set; }
        //public string BrandName { get; set; }
        //public string UOMName { get; set; }
        //public string PackSizeName { get; set; }

        // Dropdowns
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SubCategories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        //public List<SelectListItem> UOMs { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Units { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> PackSizes { get; set; } = new List<SelectListItem>();
    }
}
