using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class Advert
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public bool isActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Users Users { get; set; }
        public Cars Cars { get; set; }
    }
}
