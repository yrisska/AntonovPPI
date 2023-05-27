using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebApplication1.DTO.Review
{
    public class ReviewForPost
    {
        [Range(0, 5)]
        public double Rating { get; set; }
        [JsonIgnore] // we will grab this from token
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        [MaxLength(2000)]
        public string Text { get; set; }
        public Models.Review ToModel() => new()
        {
            Rating = Rating,
            UserId = UserId,
            Text = Text,
            BookId = BookId,
        };
    }
}
