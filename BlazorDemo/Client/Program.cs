using BlazorDemo.Client;
using BlazorDemo.Client.Dictionaries.Categories.Services;
using BlazorDemo.Client.Shared.Services;
using BlazorDemo.Client.Shared.State;
using BlazorDemo.Client.Todos.Services;
using BlazorDemo.Shared.Services;
using BlazorDemo.Shared.Todos;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<INotifier, Notifier>();
builder.Services.AddScoped<OverdueTodosState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITodoClient, TodoClient>();
builder.Services.AddScoped<ICategoryClient, CategoryClient>();

builder.Services.AddValidatorsFromAssemblyContaining<TodoCreateDtoValidator>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
