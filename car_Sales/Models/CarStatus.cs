using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class CarStatus
    {
        [Key]
        public int ID { get; set; }
        public CarParts CarParts { get; set; }
        public string Description { get; set; }
        public Cars Cars { get; set; }
        public Status Status { get; set; }
    }
}
