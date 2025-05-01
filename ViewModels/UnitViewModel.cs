using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.ViewModels
{
    public class UnitViewModel
    {
        [Required]
        [Display(Name = "Unit Name")]
        public string NameOfUnit { get; set; } = string.Empty;

        [Range(0.0000001, double.MaxValue, ErrorMessage = "Factor must be greater than 0.")]
        public double UnitFactor { get; set; }

        [Display(Name = "Is Base Unit")]
        public bool IsBaseUnit { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }
    }
}
