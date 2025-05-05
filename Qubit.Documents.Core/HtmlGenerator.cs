using Microsoft.Extensions.Logging;
using Qubit.Documents.Models.Interfaces;
using Qubit.Documents.Models.Requests;

namespace Qubit.Documents.Core;

public class HtmlGenerator(ILogger<HtmlGenerator> logger): IHtmlGenerator
{
    public async Task<string> FromRazorAsync<T>(T request) where T : BaseRequest
    {
        try
        {
            logger.LogInformation("Converting request {RequestType} to HTML", typeof(T).Name);
            
            var result = await RazorGenerator<T>.CreateHtmlAsync(request);

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error converting request {RequestType} to HTML", typeof(T).Name);
            throw;
        }
    }
}