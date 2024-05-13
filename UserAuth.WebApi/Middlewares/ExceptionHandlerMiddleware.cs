namespace UserAuth.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await context.Response.WriteAsJsonAsync(
                new
                {
                    Code = 500,
                    Message = ex.Message,
                });
        }
    }
}
