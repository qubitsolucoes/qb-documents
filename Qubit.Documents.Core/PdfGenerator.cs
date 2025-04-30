using Microsoft.Extensions.Logging;
using Qubit.Documents.Models.Interfaces;
using Qubit.Documents.Models.Requests;

namespace Qubit.Documents.Core;

public class PdfGenerator(ILogger<PdfGenerator> logger): IPdfGenerator
{
    public async Task<Stream> FromRazorAsync<T>(T request) where T : BaseRequest
    {
        try
        {
            logger.LogInformation("Converting request {RequestType} to pdf", typeof(T).Name);
            
            var result = await RazorPdfGenerator<T>.CreateAsync(request);

            return new FileStream(result, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error converting request {RequestType} to pdf", typeof(T).Name);
            throw;
        }
    }

    public async Task<Stream> FromDocxAsync<T>(T request) where T : BaseRequest
    {
        
        throw new NotImplementedException();
    }

    public Task<string> FromHtmlAsync(string html)
    {
        throw new NotImplementedException();
    }
}