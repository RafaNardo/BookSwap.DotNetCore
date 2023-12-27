using Microsoft.AspNetCore.Mvc;
using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Get
{
    public class GetCustomerEndpoint : IEndpoint
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerEndpoint(ICustomerRepository authorRepository) => _customerRepository = authorRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/customers/{id:guid}", HandleAsync)
                .WithName("GetCustomer")
                .WithTags("Customers")
                .WithDescription("Get Customer by id")
                .Produces<Customer>()
                .ProducesNotFound()
                .WithOpenApi()
                .CacheOutput(p => p.SetVaryByQuery("Id").Tag("customers"));

        public async Task<Customer> HandleAsync([FromRoute] Guid id)
        {
            return await _customerRepository.FindAsync(id);
        }
    }
}
