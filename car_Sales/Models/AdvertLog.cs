using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class AdvertLog
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
