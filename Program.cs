using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// FIX: Expanded CORS security allowances for flexible frontend testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowVueApp");

// Enables serving index.html automatically from the wwwroot folder
app.UseDefaultFiles();
app.UseStaticFiles();

// Minimal API Route Endpoints
app.MapGet("/api/todos", () => TodoService.GetAll());

app.MapGet("/api/todos/{id}", (int id) => 
    TodoService.Get(id) is TodoItem item ? Results.Ok(item) : Results.NotFound());

app.MapPost("/api/todos", (TodoItem item) => 
{
    TodoService.Add(item);
    return Results.Created($"/api/todos/{item.Id}", item);
});

app.MapPut("/api/todos/{id}", (int id, TodoItem updatedItem) => 
{
    var existingItem = TodoService.Get(id);
    if (existingItem is null) return Results.NotFound();

    updatedItem.Id = id;
    TodoService.Update(updatedItem);
    return Results.NoContent();
});

app.MapDelete("/api/todos/{id}", (int id) => 
{
    var existingItem = TodoService.Get(id);
    if (existingItem is null) return Results.NotFound();

    TodoService.Delete(id);
    return Results.NoContent();
});

app.Run();
