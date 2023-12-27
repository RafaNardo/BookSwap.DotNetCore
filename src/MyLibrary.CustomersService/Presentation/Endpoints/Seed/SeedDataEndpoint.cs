using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Seed
{
    public class SeedDataEndpoint : IEndpoint
    {
        private readonly ICustomerRepository _customerRepository;

        public SeedDataEndpoint(ICustomerRepository booksRepository)
        {
            _customerRepository = booksRepository;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPost("/api/seed", HandleAsync)
                .WithName("SeedData")
                .WithTags(" Seed")
                .WithDescription("Add a list of customers")
                .ProducesList<Customer>()
                .ProducesBadRequest()
                .WithTransaction()
                .WithOpenApi();

        public async Task<IEnumerable<Customer>> HandleAsync()
        {
            var hasAnyCustomer = await _customerRepository.AnyAsync();
            if (hasAnyCustomer)
            {
                return Enumerable.Empty<Customer>();
            }

            var customers = SeedData.GetCustomers();

            await _customerRepository.AddRangeAsync(customers);

            return customers;
        }
    }
}
