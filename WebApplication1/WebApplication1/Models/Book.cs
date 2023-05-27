namespace WebApplication1.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public Publisher? Publisher { get; set; }

        public decimal Price { get; set; }
    }


public static class BookEndpoints
{
	public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Book", () =>
        {
            return new [] { new Book() };
        })
        .WithName("GetAllBooks")
        .Produces<Book[]>(StatusCodes.Status200OK);

        routes.MapGet("/api/Book/{id}", (int id) =>
        {
            //return new Book { ID = id };
        })
        .WithName("GetBookById")
        .Produces<Book>(StatusCodes.Status200OK);

        routes.MapPut("/api/Book/{id}", (int id, Book input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateBook")
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Book/", (Book model) =>
        {
            //return Results.Created($"/Books/{model.ID}", model);
        })
        .WithName("CreateBook")
        .Produces<Book>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Book/{id}", (int id) =>
        {
            //return Results.Ok(new Book { ID = id });
        })
        .WithName("DeleteBook")
        .Produces<Book>(StatusCodes.Status200OK);
    }
}
}
