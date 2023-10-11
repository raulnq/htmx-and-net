using Microsoft.AspNetCore.Html;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text;

namespace TodoApi;

public class HtmlContentResult : IResult
{
    private readonly IHtmlContent _htmlContent;

    public HtmlContentResult(IHtmlContent htmlContent)
    {
        _htmlContent = htmlContent;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = MediaTypeNames.Text.Html;
        using (var writer = new StringWriter())
        {
            _htmlContent.WriteTo(writer, HtmlEncoder.Default);
            var html = writer.ToString();
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(html);
            return httpContext.Response.WriteAsync(html);
        }
    }
}
