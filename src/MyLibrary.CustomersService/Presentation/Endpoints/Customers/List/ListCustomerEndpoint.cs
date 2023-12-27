using Microsoft.AspNetCore.Mvc;
using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.List
{
    public class ListCustomerEndpoint : IEndpoint
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomerEndpoint(ICustomerRepository authorRepository) => _customerRepository = authorRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/customers", HandleAsync)
                .WithName("ListCustomers")
                .WithTags("Customers")
                .WithDescription("Lists customers by Filter")
                .ProducesList<Customer>()
                .WithOpenApi()
                .CacheOutput(p => p.SetVaryByQuery("Name").Tag("customers"));

        public async Task<IEnumerable<Customer>> HandleAsync(
            [FromQuery] string? name,
            [FromQuery] string? email)
        {
            var spec = new Specification<Customer>()
                .AddCriteriaIf(x => x.Name.Contains(name!), !string.IsNullOrEmpty(name))
                .AddCriteriaIf(x => x.Email.Contains(email!), !string.IsNullOrEmpty(email))
                .AddOrderBy(x => x.Name);

            return await _customerRepository.ListAsync(spec);
        }
    }
}
