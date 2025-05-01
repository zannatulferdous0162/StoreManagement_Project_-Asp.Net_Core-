using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.Models
{
    public class ItemEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Display(Name = "Unit of Measure")]
        public int UnitId { get; set; }

        [Display(Name = "Pack Size")]
        public int PackSizeId { get; set; }

        public SelectList? CategoryOptions { get; set; }
        public SelectList? SubCategoryOptions { get; set; }
        public SelectList? BrandOptions { get; set; }
        //public SelectList? UOMOptions { get; set; }
        public SelectList? UnitOptions { get; set; }

        public SelectList? PackSizeOptions { get; set; }
    }
}
