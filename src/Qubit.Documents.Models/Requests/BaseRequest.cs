using System.Reflection;

namespace Qubit.Documents.Models.Requests;

public abstract class BaseRequest
{
    /// <summary>
    /// The URLs of the fonts used in the document.
    /// </summary>
    public virtual IEnumerable<string> FontsUrls { get; } = 
        ["https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap"];
    
    public abstract Assembly ExecutingAssembly { get; }
    
    /// <summary>
    ///  The name of the resource file.
    /// </summary>
    public abstract string ResourceName { get; }
}