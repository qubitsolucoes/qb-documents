using FakeItEasy;
using Qubit.Documents.Core.Tests.Models;
using IHtmlGenerator = Qubit.Documents.Models.Interfaces.IHtmlGenerator;

namespace Qubit.Documents.Core.Tests;

public class HtmlGeneratorTests
{
    private static readonly ILogger<HtmlGenerator> Logger = A.Fake<ILogger<HtmlGenerator>>();
    private readonly IHtmlGenerator _htmlGenerator = new HtmlGenerator(Logger);
    
    [Test]
    public async Task FromRazorAsync_ShouldGenerateHtml_WhenRequestIsValidAndFileExists()
    {
        // Arrange
        var model = new TemplateModel { Title = "Test" };

        // Act
        var result = await _htmlGenerator.FromRazorAsync(model);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Does.Contain(model.Title));
        });
    }
}