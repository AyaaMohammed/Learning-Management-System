namespace API.Middleware
{
    public class CorrelationIdEnricherMiddleware
    {
        private readonly RequestDelegate _next;

        private const string CorrelationIdHeader = "X-Correlation-ID";

        public CorrelationIdEnricherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers.TryGetValue(CorrelationIdHeader, out var value)
                                                ? value.ToString() : Guid.NewGuid().ToString();

            context.Response.Headers[CorrelationIdHeader] = correlationId;

            await _next(context);
        }
    }
}
