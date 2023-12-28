using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class PriceTransactions
    {
        [Key]
        public int ID { get; set; }
        public Cars Cars { get; set; }
        public float OldPrice { get; set; }
        public float NewPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
