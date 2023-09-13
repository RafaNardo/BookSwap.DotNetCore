using BookSwap.Shared.Core.Di;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

builder.RegisterCoreServices(assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCoreServices(assembly);

app.Run();