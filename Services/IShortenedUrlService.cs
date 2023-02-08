namespace Shorty.Services.ShortenedUrl;

public interface IShortenedUrlService
{
    Task<string> Get(string shortenedUrl);
    Task<string> Create(string url);
}