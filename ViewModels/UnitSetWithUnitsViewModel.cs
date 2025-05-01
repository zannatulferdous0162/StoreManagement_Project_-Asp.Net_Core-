using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.ViewModels
{
    public class UnitSetWithUnitsViewModel
    {
        public int UnitSetId { get; set; }

        [Required]
        [Display(Name = "Unit Set Name")]
        public string NameOfUnitSet { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        public List<UnitViewModel> Units { get; set; } = new();
    }
}
