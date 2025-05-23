using PDFNote;

internal static class Sample
{
    internal static DocumentModel BuildDoc()
    {
        DocumentModel doc = new()
        {
            Title = "Document Title",
            FileName = "sample.pdf",
            Orientation = Orientation.Portrait,
            Reader = Reader.RemarkablePaperPro,
        };

        doc.Cover = new()
        {
            Header = "Document Header",
            Symbol = "𖠿",
            Color = "01579B"
        };

        doc.NumberedPages = new()
        {
            PageCount = 120,
        };

        return doc;
    }
}