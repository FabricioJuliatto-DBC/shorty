namespace Shorty.Services.ShortenedUrl;

using Shorty.Domain;
using System.Security.Cryptography;
using System.Text;

public class ShortenedUrlService : IShortenedUrlService
{
    private readonly IShortenedUrlRepository _repository;
    public ShortenedUrlService(IShortenedUrlRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Get(string shortenedUrl)
    {
        var result = await _repository.GetByShortenedUrl(shortenedUrl);
        if (result is null)
        {
            throw new UrlNotFoundException();
        }
        
        return result.OriginalUrl;
    }

    public async Task<string> Create(string originalUrl)
    {
        var sha256 = SHA256.Create();
        var shortenedUrl = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalUrl)).ToString();
        if(shortenedUrl is null)
        {
            throw new Exception($"Could not compute hash for {originalUrl}.");
        }

        var url = new Url 
        {
            OriginalUrl = originalUrl,
            ShortenedUrl = shortenedUrl,
        };

        var id = await _repository.Add(url);

        return id;
    }
}