namespace Shorty.Infrastructure.ShortenedUrl;

using Shorty.Domain;
using Shorty.Services.ShortenedUrl;

public class ShortenedUrlRepository: IShortenedUrlRepository {
    private readonly List<Url> _shortenedUrls = new List<Url>();

    public Task<string> Add(Url url)
    {
        _shortenedUrls.Add(url);
        return Task.FromResult(url.ShortenedUrl);
    }

    public Task<Url?> GetByShortenedUrl(string shortenedUrl)
    {
        return Task.FromResult(_shortenedUrls.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl));
    }
}