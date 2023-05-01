namespace CaseStudy.Employee.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RequestDelegate _nextMiddleWare;
        public AuthenticationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _nextMiddleWare = next;
            _httpClientFactory = httpClientFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            try
            {
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    var httpClient = _httpClientFactory.CreateClient("APIClient");

                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        "/api/Auth/Authenticate");
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authHeader.Split("Bearer ")[1]);
                    var response = await httpClient.SendAsync(
                        request, HttpCompletionOption.ResponseHeadersRead);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        await _nextMiddleWare(context);
                    else
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
