using CSharpWebApi.Handlers;
using FSharpWebApi;
using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using Giraffe;

using Giraffe.EndpointRouting;

var builder = WebApplication.CreateBuilder(args);
Middleware.ServiceCollectionExtensions.AddGiraffe(builder.Services);
builder.Services.AddRouting();
builder.Services.AddTransient((_) =>
{
    var func = FSharpHandlers.Dependencies.Greetings.getGreeting;
    return FuncConvert.FromFunc(func);
});

builder.Services.AddTransient(_ =>
{
    var func = FSharpHandlers.Dependencies.Clients.getClientName;
    return FuncConvert.FromFunc(func);
});

builder.Services.AddTransient(serviceProvider =>
{
    var getGreeting = serviceProvider.GetRequiredService<FSharpFunc<string, FSharpAsync<string>>>();
    var getClient = serviceProvider.GetRequiredService<FSharpFunc<int, FSharpAsync<string>>>();

    Task<string> Handler(string lang, int id) => FSharpAsync.StartAsTask(FSharpHandlers.SampleHandler.getGreeting(getGreeting, getClient, lang, id),
        FSharpOption<TaskCreationOptions>.None, FSharpOption<CancellationToken>.None);

    return Handler;
});


builder.Services.AddTransient<SampleHandler>();

var app = builder.Build();
var compositionRoot = CompositionRootModule.compose();
app.UseRouting();
app.UseGiraffe(HttpHandlers.endpoints(compositionRoot));
app.MapGet("/Giraffe/", (SampleHandler handler) => handler.GetGreeting("English", 1));
app.MapGet("/Func/", (Func<string,int, Task<string>> handler) => handler("Polish", 2));
app.MapGet("/SampleFun/", () => FSharpHandlers.FunctionWithoutDependencies.sampleFunc("SampleFunc"));

app.Run();