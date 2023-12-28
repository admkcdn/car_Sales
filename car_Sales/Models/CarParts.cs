using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class CarParts
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
