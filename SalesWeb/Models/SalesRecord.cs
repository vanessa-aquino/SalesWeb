using System.ComponentModel.DataAnnotations;
using SalesWeb.Models.Enums;

namespace SalesWeb.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        [Display(Name = "Data da venda")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Valor da venda")]
        [DisplayFormat(DataFormatString ="R${0:F2}")]
        public double Amount { get; set; }

        [Display(Name = "Status da venda")]
        public SalesStatus Status { get; set; }

        [Display(Name = "Nome do vendedor")]
        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
