using System.ComponentModel.DataAnnotations;

namespace car_Sales.Models
{
    public class Cars
    {
        [Key]
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public float Kilometer { get; set; }
        public bool Status { get; set; }
        public int HorsePower { get; set; }
        public int EngineVolume { get; set; }
        public Types Types { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public FuelType FuelType { get; set; }
    }
}
