using WebApplication1.DTO.Book;
using WebApplication1.Models;

namespace WebApplication1.Services.PublisherService
{
    public interface IPublisherService
    {
        Task<Publisher> GetPublisher(Guid id);
        Task<List<Publisher>> GetPublishers();
        Task<Publisher> CreatePublisher(PublisherForUpdate publisherForUpdate);
        Task UpdatePublisher(Guid id, PublisherForUpdate publisherForUpdate);
        Task DeletePublisher(Guid id);
    }
}
