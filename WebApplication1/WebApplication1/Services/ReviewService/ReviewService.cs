using WebApplication1.DTO.Review;
using WebApplication1.Models;
using WebApplication1.Services.AuthService;
using WebApplication1.Services.BookService;

namespace WebApplication1.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly List<Review> _reviewRepository = new();
        private readonly IAuthService _authService;
        private readonly IBookService _bookService;

        public ReviewService(IAuthService authService, IBookService bookService)
        {
            _authService = authService;
            _bookService = bookService;
            SeedReviews();
        }

        private void SeedReviews()
        {

            var task1 = _bookService.GetBooks();
            task1.Wait();
            var books = task1.Result;

            var task2 = _authService.GetUsers();
            task2.Wait();
            var users = task2.Result;

            var review1 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 4.5,
                BookName = "Big Data: Principles and best practices of scalable realtime data systems",
                BookId = books.First(x => x.Title == "Big Data: Principles and best practices of scalable realtime data systems").Id,
                UserName = "Jane Doe",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Jane Doe").Id,
                Text = "Nice book!"
            };

            var review2 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 2.1,
                BookName = "Kubernetes in Action",
                BookId = books.First(x => x.Title == "Kubernetes in Action").Id,
                UserName = "Jane Doe",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Jane Doe").Id,
                Text = "SO BORING RETURN MONEY!!!"
            };

            var review3 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 5,
                BookName = "Kubernetes in Action",
                BookId = books.First(x => x.Title == "Kubernetes in Action").Id,
                UserName = "John Smith",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "John Smith").Id,
                Text = "Amazing book"
            };

            var review4 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 3.8,
                BookName = "Big Data: Principles and best practices of scalable realtime data systems",
                BookId = books.First(x => x.Title == "Big Data: Principles and best practices of scalable realtime data systems").Id,
                UserName = "Alex Johnson",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Alex Johnson").Id,
                Text = "Informative and well-written"
            };

            var review5 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 4.2,
                BookName = "Kubernetes in Action",
                BookId = books.First(x => x.Title == "Kubernetes in Action").Id,
                UserName = "Sarah Thompson",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Sarah Thompson").Id,
                Text = "A must-read for anyone interested in Kubernetes"
            };

            var review6 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 2.5,
                BookName = "Big Data: Principles and best practices of scalable realtime data systems",
                BookId = books.First(x => x.Title == "Big Data: Principles and best practices of scalable realtime data systems").Id,
                UserName = "Robert Davis",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Robert Davis").Id,
                Text = "Not as comprehensive as I expected"
            };

            var review7 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 4.7,
                BookName = "Kubernetes in Action",
                BookId = books.First(x => x.Title == "Kubernetes in Action").Id,
                UserName = "Emily Wilson",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Emily Wilson").Id,
                Text = "Well-structured and practical"
            };

            var review8 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 3.1,
                BookName = "Big Data: Principles and best practices of scalable realtime data systems",
                BookId = books.First(x => x.Title == "Big Data: Principles and best practices of scalable realtime data systems").Id,
                UserName = "Michael Johnson",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Michael Johnson").Id,
                Text = "Decent but could be more engaging"
            };

            var review9 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 4.9,
                BookName = "Kubernetes in Action",
                BookId = books.First(x => x.Title == "Kubernetes in Action").Id,
                UserName = "Jessica Adams",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "Jessica Adams").Id,
                Text = "Highly recommended for Kubernetes enthusiasts"
            };

            var review10 = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 2.8,
                BookName = "Big Data: Principles and best practices of scalable realtime data systems",
                BookId = books.First(x => x.Title == "Big Data: Principles and best practices of scalable realtime data systems").Id,
                UserName = "David Brown",
                UserId = users.First(x => $"{x.FirstName} {x.LastName}" == "David Brown").Id,
                Text = "Lacked depth and examples"
            };

            _reviewRepository.Add(review1);
            _reviewRepository.Add(review2);
            _reviewRepository.Add(review3);
            _reviewRepository.Add(review4);
            _reviewRepository.Add(review5);
            _reviewRepository.Add(review6);
            _reviewRepository.Add(review7);
            _reviewRepository.Add(review8);
            _reviewRepository.Add(review9);
            _reviewRepository.Add(review10);
        }

        public async Task<Review> CreateReview(ReviewForPost reviewForPost)
        {
            var review = reviewForPost.ToModel();

            review.Id = Guid.NewGuid();
            var user = await _authService.GetUserDetails(review.UserId);

            review.UserName = $"{user.FirstName} {user.LastName}";

            var book = await _bookService.GetBook(review.BookId);

            review.BookName = book.Title;

            _reviewRepository.Add(review);

            return await Task.FromResult(review);
        }

        public async Task DeleteReview(Guid id)
        {
            var review = _reviewRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No review with such id");

            _reviewRepository.Remove(review);

            await Task.CompletedTask;
        }

        public async Task<Review> GetReview(Guid id)
        {
            var review = _reviewRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No review with such id");

            return await Task.FromResult(review);
        }

        public async Task<List<BookReview>> GetReviewsByBookId(Guid id)
        {
            var reviews = _reviewRepository.Where(x => x.BookId == id)
                .Select(x => new BookReview { Id = x.Id, Rating = x.Rating, Text = x.Text, UserId = x.UserId, UserName = x.UserName})
                .ToList() ?? throw new KeyNotFoundException("No review with such bookId");

            return await Task.FromResult(reviews);
        }

        public async Task<List<UserReview>> GetReviewsByUserId(Guid id)
        {
            var reviews = _reviewRepository.Where(x => x.UserId == id)
                .Select(x => new UserReview { Id = x.Id, Rating = x.Rating, Text = x.Text, BookId = x.BookId, BookName = x.BookName})
                .ToList() ?? throw new KeyNotFoundException("No review with such userId");

            return await Task.FromResult(reviews);
        }

        public async Task UpdateReview(Guid id, ReviewForUpdate reviewForUpdate)
        {
            var review = _reviewRepository.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No review with such id");

            review.Text = reviewForUpdate.Text;
            review.Rating = reviewForUpdate.Rating;

            await Task.CompletedTask;
        }
    }
}
