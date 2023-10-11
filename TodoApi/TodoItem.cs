namespace TodoApi;
public class TodoItem
{
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public string? Name { get; set; }
};

record AddTodoItem(string? Name);
