using BookSwap.BooksService.Modules.Books.Commands.Add;
using BookSwap.BooksService.Modules.Books.Commands.Delete;
using BookSwap.BooksService.Modules.Books.Commands.Update;
using BookSwap.BooksService.Modules.Books.Entities;
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
            .WithDescription("Lists books by Filter")
            .ProducesList<Book>()
            .WithOpenApi();

        endpoints.MapPost("/api/books", async ([FromServices] IMediator mediator, AddBookCommand command) => {
                var id = await mediator.Send(command);
                return Results.Created($"/api/books/{id}", new CreatedResponse(id));
            })
            .WithName("AddBook")
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
            .WithDescription("Gets a book by id")
            .ProducesNotFound()
            .WithOpenApi();

        endpoints.MapDelete("/api/books/{id:guid}", async ([FromServices] IMediator mediator, Guid id) => {
                await mediator.Send(new DeleteBookCommand(id));
                return Results.Ok();
            })
            .WithName("DeleteBook")
            .WithDescription("Delete a book by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithOpenApi();

        return endpoints;
    }

    public IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration) => builder;
}

