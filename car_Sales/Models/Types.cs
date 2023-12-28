using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class Types
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
