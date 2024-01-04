namespace car_Sales.DTOs
{
    public class CarDto
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Kilometer { get; set; }
        public bool Status { get; set; }
        public int HorsePower { get; set; }
        public decimal EngineVolume { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }
        public string? TransmissionType { get; set; }
        public string? FuelType { get; set; }
        public string? CarImage { get; set; }
    }

}
