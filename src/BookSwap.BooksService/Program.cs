using System.Reflection;
using BookSwap.Shared.Core.App;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

builder.RegisterCoreServices(assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCoreServices(assembly);

app.Run();