var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");
var database = builder.AddSqlServerContainer("database", "Password*123", 1499);

builder
    .AddProject<Projects.MyLibrary_BooksService>("Books")
    .WithReference(cache)
    .WithReference(database);

builder.AddProject<Projects.MyLibrary_CustomersService>("Customers")
    .WithReference(cache)
    .WithReference(database);

builder.Build().Run();
