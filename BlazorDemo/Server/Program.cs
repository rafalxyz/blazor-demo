using BlazorDemo.Server.Exceptions;
using BlazorDemo.Server.Services;
using BlazorDemo.Shared.Services;
using BlazorDemo.Shared.Todos;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddValidatorsFromAssemblyContaining<TodoCreateDtoValidator>();

builder.Services.AddProblemDetails(config =>
{
    config.Map<BusinessException>(ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        Title = "Business validation rule broken",
        Status = StatusCodes.Status400BadRequest,
        Detail = ex.Message,
        Type = $"https://blazor-demo/{ex.Code}"
    });
});

var app = builder.Build();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
