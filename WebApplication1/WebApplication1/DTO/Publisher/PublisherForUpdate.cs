namespace WebApplication1.DTO.Book
{
    public class PublisherForUpdate
    {
        public string Name { get; set; }
        public Models.Publisher ToModel() => new()
        {
            Name = Name
        };
    }
}
