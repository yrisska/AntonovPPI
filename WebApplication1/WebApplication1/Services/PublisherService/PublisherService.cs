using WebApplication1.DTO.Book;
using WebApplication1.Models;

namespace WebApplication1.Services.PublisherService
{
    public class PublisherService : IPublisherService
    {
        private readonly List<Publisher> _publisherRepository = new()
        {
            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Manning Publications"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Microsoft Press"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Apress"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "O'Reilly Media"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Packt Publishing"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Golden Gate Press"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Azure Publishing"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Clear Books"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Silver Star Publishers"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Harmony House"
            },

            new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Evergreen Press"
            },
        };

        public async Task<Publisher> CreatePublisher(PublisherForUpdate publisherForUpdate)
        {
            var publisher = publisherForUpdate.ToModel();
            publisher.Id = Guid.NewGuid();
            _publisherRepository.Add(publisher);

            return await Task.FromResult(publisher);
        }

        public async Task DeletePublisher(Guid id)
        {

            var publisher = _publisherRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No publisher with such id");

            _publisherRepository.Remove(publisher);

            await Task.CompletedTask;
        }

        public async Task<Publisher> GetPublisher(Guid id)
        {
            var publisher = _publisherRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No publisher with such id");

            return await Task.FromResult(publisher);
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await Task.FromResult(_publisherRepository);
        }

        public async Task UpdatePublisher(Guid id, PublisherForUpdate publisherForUpdate)
        {
            var publisher = _publisherRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No publisher with such id");

            publisher.Name = publisherForUpdate.Name;

            await Task.CompletedTask;
        }
    }
}
