global using BookSwap.Shared.Core.EndpointFilters;
global using BookSwap.Shared.Core.Endpoints;
global using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.DI;
using FluentValidation;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddSwagger();

builder.Services.AddRedis(assembly, configuration);

builder.Services.AddModules(assembly, configuration);

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddEndpoints();

builder.Services.AddOutputCache(options => { options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(5); });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.ConfigureSwagger();

app.UseOutputCache();

app.UseEndpoints();

app.MigrateDatabases(assembly);

app.Run();