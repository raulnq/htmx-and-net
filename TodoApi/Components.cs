using Microsoft.AspNetCore.Html;

namespace TodoApi;

public static class Components
{
    public static IHtmlContent Document(IHtmlContentContainer children)
    {
        var builder = new HtmlContentBuilder();
        builder.AppendHtml("<!doctype html>");
        builder.AppendHtml("<html>");
        builder.AppendHtml("<head>");
        builder.AppendHtml("<meta charset=\"UTF-8\">");
        builder.AppendHtml("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        builder.AppendHtml("<script src=\"https://unpkg.com/htmx.org@1.9.6\" integrity=\"sha384-FhXw7b6AlE/jyjlZH5iHa/tTe9EpJ1Y55RjcgPbjeWMskSxZt1v9qkxLJWNJaGni\" crossorigin=\"anonymous\"></script>");
        builder.AppendHtml("<script src=\"https://unpkg.com/htmx.org/dist/ext/json-enc.js\"></script>");
        builder.AppendHtml("</head>");
        builder.AppendHtml(children);
        builder.AppendHtml("</html>");
        return builder;
    }

    public static IHtmlContent TodoList(IEnumerable<TodoItem> todoItems)
    {
        var builder = new HtmlContentBuilder();
        builder.AppendHtml("<div>");
        foreach (var todoItem in todoItems)
        {
            builder.AppendHtml(TodoItem(todoItem));
        }
        builder.AppendHtml(TodoForm());
        builder.AppendHtml("</div>");
        return builder;
    }

    public static IHtmlContent TodoItem(TodoItem todoItem)
    {
        var isCompleted = string.Empty;
        if (todoItem.IsCompleted)
        {
            isCompleted = "checked";
        }
        var builder = new HtmlContentBuilder();
        builder.AppendHtml("<div>");
        builder.AppendHtml("<p>");
        builder.AppendFormat("{0}", todoItem.Name);
        builder.AppendHtml("</p>");
        builder.AppendHtml($"<input type=\"checkbox\" {isCompleted} hx-post=\"/todos/{todoItem.Id}/toggle\" hx-target=\"closest div\" hx-swap=\"outerHTML\"/>");
        builder.AppendHtml($"<button hx-delete=\"/todos/{todoItem.Id}\" hx-target=\"closest div\" hx-swap=\"outerHTML\">X</button>");
        builder.AppendHtml("</div>");
        return builder;
    }

    public static IHtmlContent TodoForm()
    {
        var builder = new HtmlContentBuilder();
        builder.AppendHtml("<form hx-post=\"/todos\" hx-swap=\"beforebegin\" hx-ext=\"json-enc\">");
        builder.AppendHtml("<input type=\"text\" name=\"name\">");
        builder.AppendHtml("<button type=\"submit\">Add</button>");
        builder.AppendHtml("</form>");
        return builder;
    }
}

