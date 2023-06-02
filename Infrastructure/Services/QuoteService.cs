using Dapper;
using Domain.Dtos;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class QuoteService
{
    private readonly DapperContext context;
    private readonly IFileService fileService;

    public QuoteService(DapperContext context, IFileService fileService)
    {
        this.context = context;
        this.fileService = fileService;
    }
    public List<GetQuoteDto> GetQuoteDto()
    {
        using (var conn = context.CreateConnection())
        {
            var sql = "select id Id, author Author, quote_text QuoteText, category_id CategoryId, file_name FileName from quotes";
            return conn.Query<GetQuoteDto>(sql).ToList();
        }
    }
    public GetQuoteDto AddQuote(AddQuoteDto quote)
    {
        using (var conn = context.CreateConnection())
        {
            var filename = fileService.CreateFile("images", quote.File);
            var sql = "insert into quotes(author, quote_text, category_id, file_name) values(@author, @quotetext, @categoryid, @filename) returning id";
            var result = conn.ExecuteScalar<int>(sql, new
            {
                quote.Author,
                quote.QuoteText,
                quote.CategoryId,
                filename
            });
            return new GetQuoteDto()
            {
                Author = quote.Author,
                QuoteText = quote.QuoteText,
                CategoryId = quote.CategoryId,
                FileName = filename,
                Id = result
            }; 
        }
    }
}
