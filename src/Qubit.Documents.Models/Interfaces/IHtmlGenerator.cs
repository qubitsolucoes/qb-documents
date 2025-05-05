using Qubit.Documents.Models.Requests;

namespace Qubit.Documents.Models.Interfaces;

public interface IHtmlGenerator
{
    Task<string> FromRazorAsync<T>(T request) where T : BaseRequest;
}