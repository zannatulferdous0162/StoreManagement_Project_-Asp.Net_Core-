using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.Models
{
    public class UnitSet
    {
        public int UnitSetId { get; set; }

        [Required(ErrorMessage = "Unit set name is required.")]
        [Display(Name = "Unit Set Name")]
        public string NameOfUnitSet { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        // Navigation property
        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
