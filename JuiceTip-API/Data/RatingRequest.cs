using System.ComponentModel.DataAnnotations;

namespace JuiceTip_API.Data
{
    public class RatingRequest
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        public Guid RatingId { get; set; }
    }
}
