using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public Cars Cars { get; set; }
        public string imagePath { get; set; }
    }
}
