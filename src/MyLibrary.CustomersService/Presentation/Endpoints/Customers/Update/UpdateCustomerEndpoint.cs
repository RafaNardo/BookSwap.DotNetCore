using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.CustomersService.Domain.ValueObjects;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Update
{
    public class UpdateCustomerEndpoint : IEndpoint
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IOutputCacheStore _outputCacheStore;

        public UpdateCustomerEndpoint(ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore)
        {
            _customerRepository = customerRepository;
            _outputCacheStore = outputCacheStore;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPut("/api/customers/{id:guid}", HandleAsync)
                .WithName("UpdateCustomer")
                .WithTags("Customers")
                .WithDescription("Updates an existing Customer")
                .ProducesOk()
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .WithTransaction()
                .WithValidator<UpdateCustomerRequest>()
                .WithOpenApi();

        public async Task<Guid> HandleAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateCustomerRequest request, 
            CancellationToken ct)
        {
            var customer = await _customerRepository.FindAsync(id);

            customer.Update(request.Name, request.Email, request.Phone);

            var address = new Address(
                request.Address.State,
                request.Address.City,
                request.Address.Street,
                request.Address.Number,
                request.Address.ZipCode);

            customer.UpdateAddress(address);
            
            await _customerRepository.UpdateAsync(customer);

            await _outputCacheStore.EvictByTagAsync("customers", ct);

            return customer.Id;
        }
    }
}
