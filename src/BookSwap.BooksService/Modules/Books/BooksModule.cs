using BookSwap.BooksService.Modules.Books.Commands.Add;
using BookSwap.BooksService.Modules.Books.Commands.Delete;
using BookSwap.BooksService.Modules.Books.Commands.Genres.Add;
using BookSwap.BooksService.Modules.Books.Commands.Genres.Delete;
using BookSwap.BooksService.Modules.Books.Commands.Genres.Update;
using BookSwap.BooksService.Modules.Books.Commands.Update;
using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Queries.Genres.Get;
using BookSwap.BooksService.Modules.Books.Queries.Genres.List;
using BookSwap.BooksService.Modules.Books.Queries.Get;
using BookSwap.BooksService.Modules.Books.Queries.List;
using BookSwap.Shared.Core.Models;
using BookSwap.Shared.Core.Modules;
using BookSwap.Shared.Core.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books;

public class BooksModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/books", async ([FromServices] IMediator mediator, [AsParameters] ListBookRequest request) => {
                return Results.Ok(await mediator.Send(request));
            })
            .WithName("ListBooks")
            .WithTags("Books")
            .WithDescription("Lists books by Filter")
            .ProducesList<Book>()
            .WithOpenApi();

        endpoints.MapPost("/api/books", async ([FromServices] IMediator mediator, AddBookCommand command) => {
                var id = await mediator.Send(command);
                return Results.Created($"/api/books/{id}", new CreatedResponse(id));
            })
            .WithName("AddBook")
            .WithTags("Books")
            .WithDescription("Adds a new book")
            .ProducesCreated()
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .WithOpenApi();

        endpoints.MapPut("/api/books/{id:guid}", async ([FromServices] IMediator mediator, [FromRoute] Guid id, UpdateBookCommand command) => {
                await mediator.Send(command with { Id = id });

                return Results.Ok();
            })
            .WithName("UpdateBook")
            .WithTags("Books")
            .WithDescription("Update a new book")
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .ProducesOk()
            .WithOpenApi();


        endpoints.MapGet("/api/books/{id:guid}", async ([FromServices] IMediator mediator, [FromRoute] Guid id) => {
                var request = new GetBookQuery(id);
                return Results.Ok(await mediator.Send(request));
            })
            .WithName("GetBook")
            .WithTags("Books")
            .WithDescription("Gets a book by id")
            .ProducesNotFound()
            .WithOpenApi();

        endpoints.MapDelete("/api/books/{id:guid}", async ([FromServices] IMediator mediator, Guid id) => {
                await mediator.Send(new DeleteBookCommand(id));
                return Results.Ok();
            })
            .WithName("DeleteBook")
            .WithTags("Books")
            .WithDescription("Delete a book by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithOpenApi();

        
        endpoints.MapDelete("/api/genres/{id:guid}", async ([FromServices] IMediator mediator, Guid id) => {
                await mediator.Send(new DeleteGenreCommand(id));
                return Results.Ok();
            })
            .WithName("DeleteBookGenre")
            .WithTags("Genres")
            .WithDescription("Delete a book genre by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithOpenApi();
        
        
        endpoints.MapPost("/api/genres", async ([FromServices] IMediator mediator, AddGenreCommand command) => {
                var id = await mediator.Send(command);
                return Results.Created($"/api/genres/{id}", new CreatedResponse(id));
            })
            .WithName("AddBookGenre")
            .WithDescription("Adds a new book genre")
            .WithTags("Genres")
            .ProducesCreated()
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .WithOpenApi();

        endpoints.MapPut("/api/genres/{id:guid}", async ([FromServices] IMediator mediator, [FromRoute] Guid id, UpdateGenreCommand command) => {
                await mediator.Send(command with { Id = id });

                return Results.Ok();
            })
            .WithName("UpdateBookGenre")
            .WithDescription("Update a new book genre")
            .WithTags("Genres")
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .ProducesOk()
            .WithOpenApi();
        
        endpoints.MapGet("/api/genres", async ([FromServices] IMediator mediator, [AsParameters] ListBookGenreQuery request) => {
                return Results.Ok(await mediator.Send(request));
            })
            .WithName("ListBookGenres")
            .WithTags("Genres")
            .WithDescription("Lists books genres by Filter")
            .ProducesList<Book>()
            .WithOpenApi();
        
        endpoints.MapGet("/api/genres/{id:guid}", async ([FromServices] IMediator mediator, [FromRoute] Guid id) => {
                var request = new GetBookGenreQuery(id);
                return Results.Ok(await mediator.Send(request));
            })
            .WithName("GetBookGenre")
            .WithTags("Genres")
            .WithDescription("Gets a book genre by id")
            .ProducesNotFound()
            .WithOpenApi();
        return endpoints;
    }

    public IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration) => builder;
}

