namespace car_Sales.DTOs
{
    public class AdvertDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Horsepower { get; set; }
        //public IFormFile[] Images { get; set; }
        public string Kilometer { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public CheckedItemDto FuelType { get; set; }
        //public string FuelType { get; set; }
        public CheckedItemDto TransmissionType { get; set; }
        //public string TransmissionType { get; set; }
    }
}
