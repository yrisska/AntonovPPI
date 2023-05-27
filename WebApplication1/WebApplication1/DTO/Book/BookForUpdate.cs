using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO.Book
{
    public class BookForUpdate
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public DateTime PublishedOn { get; set; }

        [Required]
        public Guid PublisherId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Models.Book ToModel() => new()
        {
            Title = Title,
            Description = Description,
            Price = Price,
            PublishedOn = PublishedOn
        };
    }
}
