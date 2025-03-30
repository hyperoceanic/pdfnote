using PDFNote.Pages;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PDFNote.Lib;

public class DocumentWriter
{
    private readonly DocumentModel _model;

    public DocumentWriter(DocumentModel model)
    {
        _model = model;
        Settings.License = LicenseType.Community;
    }

    private IPageSetWriter GetCoverWriter(DocumentModel model)
    {
        return new DefaultCoverWriter(model);
    }

    public void Create()
    {
        Console.WriteLine($"Creating {_model.FileName}");

        DocState state = new();

        List<IPageSetWriter> pageWriters = new();

        if (_model.Cover != null) pageWriters.Add(GetCoverWriter(_model));

        var result = Document.Create(container =>
            {
                pageWriters.ForEach(p => p.DocumentStart(container, state));
                pageWriters.Reverse();
                pageWriters.ForEach(p => p.DocumentEnd(container, state));
            })
            .WithSettings(new DocumentSettings
            {
                PdfA = false,
                CompressDocument = true,
                ImageCompressionQuality = ImageCompressionQuality.High,
                ImageRasterDpi = DocUtil.DPI(_model),
                ContentDirection = ContentDirection.LeftToRight
            });

        result.GeneratePdf(_model.FileName);
    }
}