using FakeItEasy;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Qubit.Documents.Core.Tests.Models;
using Qubit.Documents.Models.Interfaces;

namespace Qubit.Documents.Core.Tests;

public class PdfGeneratorTests
{
    private static readonly ILogger<PdfGenerator> Logger = A.Fake<ILogger<PdfGenerator>>();
    private readonly IPdfGenerator _pdfGenerator = new PdfGenerator(Logger);

    [Test]
    public async Task FromRazorAsync_ShouldGeneratePdf_WhenRequestIsValidAndFileExists()
    {
        // Arrange
        var model = new TemplateModel { Title = "Test" };

        // Act
        await using var pdfStream = await _pdfGenerator.FromRazorAsync(model);

        // Assert
        using var pdfReader = new PdfReader(pdfStream);
        using var pdfDocument = new PdfDocument(pdfReader);
        var pdfPage = pdfDocument.GetPage(1);
        var pageText = PdfTextExtractor.GetTextFromPage(pdfPage, new SimpleTextExtractionStrategy());
        
        Assert.Multiple(() =>
        {
            Assert.That(pdfStream.Length, Is.GreaterThan(0));
            Assert.That(pageText, Does.Contain(model.Title));
        });
    }
}