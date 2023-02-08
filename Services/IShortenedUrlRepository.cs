namespace Shorty.Services.ShortenedUrl;

using Shorty.Domain;

public interface IShortenedUrlRepository
{
    Task<string> Add(Url url);
    Task<Url?> GetByShortenedUrl(string shortenedUrl);
}