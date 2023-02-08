using Microsoft.AspNetCore.Mvc;
using Shorty.Services.ShortenedUrl;

namespace Shorty.Controllers.ShortenedUrl;

[ApiController]
[Route("url")]
public class ShortenedUrlController : ControllerBase
{
    private readonly ILogger<ShortenedUrlController> _logger;
    private readonly IShortenedUrlService _urlService;

    public ShortenedUrlController(
        ILogger<ShortenedUrlController> logger,
        IShortenedUrlService urlService
    )
    {
        _logger = logger;
        _urlService = urlService;
    }

    [HttpGet]
    [Route("{url}")]
    [ProducesResponseType(StatusCodes.Status301MovedPermanently)]
    public async Task<ActionResult> Get(string url)
    {
        try
        {
            var result = await _urlService.Get(url);
            return RedirectPermanent(result);
        } catch (UrlNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateUrlDto dto)
    {
        var shortenedUrlLocation = await _urlService.Create(dto.Url);
        return Created(shortenedUrlLocation, new {
            url = shortenedUrlLocation
        });
    }
}
