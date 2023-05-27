namespace WebApplication1.Models
{
    public class Review : BaseEntity
    {
        public double Rating { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Text { get; set; }
    }
}
