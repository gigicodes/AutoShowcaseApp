namespace AutoShowcaseApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        //public int Mileage { get; set; }
        //public string FuelType { get; set; } = string.Empty;
        //public string Transmission { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = "/images/cars/default.jfif";
        public bool IsAvailable { get; set; } = true;
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}

