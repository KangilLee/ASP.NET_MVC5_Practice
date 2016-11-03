using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InvoiceBase
    {
        [Key]
        [Display(Name = "Invoice number")]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Invoice date")]
        [DisplayFormat(DataFormatString = "{0:dddd, MMMM d, yyyy}")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Billing Address")]
        [StringLength(70)]
        public string BillingAddress { get; set; }

        [Display(Name = "Billing City")]
        [StringLength(40)]
        public string BillingCity { get; set; }

        [Display(Name = "Billing State")]
        [StringLength(40)]
        public string BillingState { get; set; }

        [Display(Name = "Billing Country")]
        [StringLength(40)]
        public string BillingCountry { get; set; }

        [Display(Name = "Billing Postal Code")]
        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Invoice total")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal Total { get; set; }
    }


    public class InvoiceWithDetail : InvoiceBase
    {

        public InvoiceWithDetail()
        {
            InvoiceLines = new List<InvoiceLineWithDetail>();
        }

        [Display(Name = "Customer Name")]
        public string CustomerFirstName { get; set; }

        
        public string CustomerLastName { get; set; }

        
        public string CustomerCity { get; set; }

        public string CustomerState { get; set; }

        [Display(Name = "Sales representative")]
        public string CustomerEmployeeFirstname { get; set; }

        public string CustomerEmployeeLastname { get; set; }


        public IEnumerable<InvoiceLineWithDetail> InvoiceLines { get; set; }

    }
}