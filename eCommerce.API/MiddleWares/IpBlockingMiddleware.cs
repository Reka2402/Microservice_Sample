namespace eCommerce.API.MiddleWares
{
    public class IpBlockingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _blockedIps = new() { "192.168.1.1" };

        public IpBlockingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (_blockedIps.Contains(context.Connection.RemoteIpAddress?.ToString()))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Denied");
                return;
            }
            await _next(context);
        }
    }
}
