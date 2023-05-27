namespace WebApplication1.DTO.Review
{
    public class UserReview
    {
        public Guid Id { get; set; }
        public double Rating { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Text { get; set; }
    }
}
