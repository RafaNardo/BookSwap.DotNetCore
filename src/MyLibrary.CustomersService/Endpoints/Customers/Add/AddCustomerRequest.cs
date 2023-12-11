using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.CustomersService.Domain.ValueObjects;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.CustomersService.Endpoints.Customers.Add
{
    public class AddCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public AddCustomerAddressRequest Address { get; set; } = null!;
    }

    public class AddCustomerEndpoint : IEndpoint
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IOutputCacheStore _outputCacheStore;

        public AddCustomerEndpoint(ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore)
        {
            _customerRepository = customerRepository;
            _outputCacheStore = outputCacheStore;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPost("/api/customers", HandleAsync)
                .WithName("AddCustomer")
                .WithTags("Customers")
                .WithDescription("Adds a new Customer")
                .ProducesCreated()
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .WithTransaction()
                .WithValidator<AddCustomerValidator>()
                .WithOpenApi();

        public async Task<Guid> HandleAsync([FromBody] AddCustomerRequest request, CancellationToken ct)
        {
            var address = new Address(
                request.Address.State,
                request.Address.City,
                request.Address.Street,
                request.Address.Number,
                request.Address.ZipCode);

            var customer = new Customer(request.Name, request.Email, address);

            await _customerRepository.AddAsync(customer);

            await _outputCacheStore.EvictByTagAsync("customers", ct);

            return customer.Id;
        }
    }
}
