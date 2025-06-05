using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using iText.Html2pdf;
using Qubit.Documents.Models;
using Qubit.Documents.Models.Requests;
using RazorLight;

namespace Qubit.Documents.Core;

internal static class RazorGenerator<T> where T : BaseRequest
{
    private static readonly ConcurrentDictionary<Assembly, RazorLightEngine> EngineCache = new();
    
    public static async Task<string> CreatePdfAsync(T request)
    {
        var tempFile = Path.GetTempFileName();

        var html = await CreateHtmlAsync(request);

        await using var outputFileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write);
        await using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(html));
        HtmlConverter.ConvertToPdf(inputStream, outputFileStream);

        return tempFile;
    }

    public static Task<string> CreateHtmlAsync(T request)
    {
        var resourceNames = request.ExecutingAssembly.GetManifestResourceNames();
        
        if(!resourceNames.Contains(request.ResourceName))
            throw new QubitDocumentsException("RazorPdfGenerator - The requested template could not be found.");

        var razorLight = EngineCache.GetOrAdd(request.ExecutingAssembly, _ => new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(request.ExecutingAssembly)
            .Build());
        
        return razorLight.CompileRenderAsync(request.ResourceName, request);
    }
}