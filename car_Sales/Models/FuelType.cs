using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class FuelType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
