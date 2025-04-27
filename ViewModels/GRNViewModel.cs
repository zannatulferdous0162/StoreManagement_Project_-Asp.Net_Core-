using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement_Project.ViewModels
{
    public class GRNViewModel
    {
        internal int GRNId;

        public string? GRNNumber { get; set; } = string.Empty;
        public DateTime ReceivedDate { get; set; } = DateTime.Now;
        [Display(Name = "Challan Date")]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "Challan Number")]
        public string InvoiceNo { get; set; } = string.Empty;
        [Display(Name = "Note")]
        public string ReceivedBy { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public int PurchaseOrderId { get; set; }

        public int? WarehouseId { get; set; }

        [Display(Name = "Warehouse Name")]
        public string? Name { get; set; }

        [Display(Name = "Location")]
        public int? LocationComponentId { get; set; }
        public List<GRNItemViewModel> GRNItems { get; set; } = new();
        public List<SelectListItem> Suppliers { get; set; } = new();
        public List<SelectListItem> PurchaseOrders { get; set; } = new();
        public List<Warehouse> Warehouses { get; set; } = new ();
        public List<SelectListItem> LocationComponents { get; set; } = new();
    }
}
