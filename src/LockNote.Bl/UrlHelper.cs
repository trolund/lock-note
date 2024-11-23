using Microsoft.AspNetCore.Http;

namespace LockNote.Bl;

public class UrlHelper(IHttpContextAccessor httpContextAccessor)
{
    public string GetBaseUrl()
    {
        var request = httpContextAccessor.HttpContext?.Request;
        return request == null ? string.Empty : $"{request.Scheme}://{request.Host}{request.PathBase}";
    }
}