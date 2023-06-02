using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly QuoteService quoteService;

    public FileUploadController(IWebHostEnvironment webHostEnvironment, QuoteService quoteService)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.quoteService = quoteService;
    }
    [HttpPost("Upload")]
    public GetQuoteDto Upload([FromForm]AddQuoteDto quote)
    {
        return quoteService.AddQuote(quote);    
    }
    [HttpGet("Get List")]
    public List<GetQuoteDto> GetStrings()
    {
        return quoteService.GetQuoteDto();
    }
}