using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.Book;
using WebApplication1.Models;
using WebApplication1.Services.PublisherService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {

        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _publisherService.GetPublishers();
        }

        [HttpGet("{id}")]
        public async Task<Publisher> GetPublisherById(Guid id)
        {
            return await _publisherService.GetPublisher(id);
        }

        [HttpPost]
        public async Task<Publisher> CreatePublisher([FromBody] PublisherForUpdate publisherForUpdate)
        {
            return await _publisherService.CreatePublisher(publisherForUpdate);
        }

        [HttpPut("{id}")]
        public async Task UpdatePublisher(Guid id, [FromBody] PublisherForUpdate publisherForUpdate)
        {
            await _publisherService.UpdatePublisher(id, publisherForUpdate);
        }

        [HttpDelete("{id}")]
        public async Task DeletePublisher(Guid id)
        {
            await _publisherService.DeletePublisher(id);
        }
    }
}
