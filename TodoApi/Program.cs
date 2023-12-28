using Microsoft.AspNetCore.Http.HttpResults;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();

var app = builder.Build();

var db = new List<TodoItem>()
{
    new TodoItem() { Id = Guid.NewGuid(), Name = "abc", IsCompleted = true },
    new TodoItem() { Id = Guid.NewGuid(), Name = "123", IsCompleted = false },
};

app.MapGet("/", () =>
{
    return new RazorComponentResult<DocumentComponent>();
});

app.MapGet("/todos", () => new RazorComponentResult<TodoItemListComponent>(new { Model = db }));

app.MapPost("/todos/{id}/toggle", (string id) =>
{
    var todo = db.First(t => t.Id.ToString() == id);
    todo.IsCompleted = !todo.IsCompleted;
    return new RazorComponentResult<TodoItemComponent>(new { Model = todo });
});

app.MapDelete("/todos/{id}", (string id) =>
{
    var todo = db.First(t => t.Id.ToString() == id);
    db.Remove(todo);
});


app.MapPost("/todos", (AddTodoItem command) =>
{
    var todo = new TodoItem { Id = Guid.NewGuid(), Name = command.Name, IsCompleted = false };
    db.Add(todo);
    return new RazorComponentResult<TodoItemComponent>(new { Model = todo });
});

app.Run();