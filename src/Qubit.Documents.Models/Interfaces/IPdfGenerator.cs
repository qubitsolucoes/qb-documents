using Qubit.Documents.Models.Requests;

namespace Qubit.Documents.Models.Interfaces;

public interface IPdfGenerator
{
    Task<Stream> FromRazorAsync<T>(T request) where T : BaseRequest;
    Task<Stream> FromDocxAsync<T>(T request) where T : BaseRequest;
    Task<string> FromHtmlAsync(string html);
}