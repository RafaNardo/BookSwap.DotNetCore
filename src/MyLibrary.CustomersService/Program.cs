global using MyLibrary.Shared.Core.EndpointFilters;
global using MyLibrary.Shared.Core.Endpoints;
using MyLibrary.Shared.Core.DI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

builder.AddServiceDefaults();

builder.AddServiceDependencies(assembly);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.ConfigureSwagger();

app.UseOutputCache();

app.UseEndpoints();

app.MigrateDatabases(assembly);

app.Run();
