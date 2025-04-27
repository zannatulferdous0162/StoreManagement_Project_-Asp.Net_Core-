using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.Models
{
    public class Unit
    {
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Unit name is required.")]
        [Display(Name = "Unit Name")]
        public string NameOfUnit { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Unit Set")]
        public int UnitSetId { get; set; }

        [Display(Name = "Factor to Base Unit")]
        [Range(0.0000001, double.MaxValue, ErrorMessage = "Factor must be greater than 0.")]
        public double UnitFactor { get; set; }

        [Display(Name = "Is Base Unit")]
        public bool IsBaseUnit { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }


        [Display(Name = "Unit Set")]
        public virtual UnitSet? UnitSet { get; set; }

        public virtual ICollection<PurchaseOrderItem>? PurchaseOrderItems { get; set; }

    }
}
