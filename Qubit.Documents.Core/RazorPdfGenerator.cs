using Qubit.Documents.Models;
using Qubit.Documents.Models.Requests;
using RazorLight;

namespace Qubit.Documents.Core;

internal static class RazorPdfGenerator<T> where T : BaseRequest
{
    public static async Task<string> CreateAsync(T request)
    {
        var tempFile = Path.GetTempFileName();
        
        var resourceNames = request.ExecutingAssembly.GetManifestResourceNames();
        
        if(!resourceNames.Contains(request.ResourceName))
            throw new QubitDocumentsException("RazorPdfGenerator - The requested template could not be found.");
        
        var razorLight = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(request.ExecutingAssembly)
            .UseMemoryCachingProvider()
            .Build();
        
        var html =  await razorLight.CompileRenderAsync(request.ResourceName, request);
        
        await using var outputFileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write);
        iText.Html2pdf.HtmlConverter.ConvertToPdf(html, outputFileStream);
        
        return tempFile;
    }
}