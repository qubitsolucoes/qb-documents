using Newtonsoft.Json;

namespace Qubit.Documents.Models;

[Serializable]
public class QubitDocumentsException: ApplicationException
{
    public QubitDocumentsException(){}
    public QubitDocumentsException(string message) : base(message){}
    public QubitDocumentsException(string message, Exception innerException) : base(message, innerException){}
    public QubitDocumentsException(IEnumerable<string> messages) : base(JsonConvert.SerializeObject(messages)) { }
}