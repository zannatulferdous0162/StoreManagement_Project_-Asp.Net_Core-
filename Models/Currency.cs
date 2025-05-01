using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement_Project.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "Currency name is required.")]
        [Display(Name = "Currency Name")]
        public string NameOfCurrency { get; set; } = string.Empty;

        [Display(Name ="Symbol")]
        public string SymbolOfCurrency { get; set; } = string.Empty;

        [Display(Name = "Exchange Rate")]
        [Range(0.0000001, double.MaxValue, ErrorMessage = "Exchange rate must be greater than 0.")]
        public double ExchangeRate { get; set; }

        [Display(Name = "Is Base Currency")]
        public bool IsBaseCurrency { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}
