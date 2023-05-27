namespace WebApplication1.DTO.Review
{
    public class BookReview
    {
        public Guid Id { get; set; }
        public double Rating { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
