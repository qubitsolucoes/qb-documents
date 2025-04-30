using System.Reflection;
using Qubit.Documents.Models.Requests;

namespace Qubit.Documents.Core.Tests.Models;

public class TemplateModel: BaseRequest
{
    public string Title { get; set; }

    public override Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
    public override string ResourceName => "Qubit.Documents.Core.Tests.Templates.Template.cshtml";
}