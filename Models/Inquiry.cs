namespace AutoShowcaseApp.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
