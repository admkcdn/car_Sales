using car_Sales.Models;

namespace car_Sales.DTOs
{
    public class AdvertPhotoDto
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public bool isActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Users Users { get; set; }
        public Cars Cars { get; set; }
        public List<string> Images { get; set; }
    }
}
