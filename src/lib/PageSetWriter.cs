using QuestPDF.Infrastructure;

namespace PDFNote.Pages;

public class DocState
{
}

public interface IPageSetWriter
{
    DocState DocumentStart(IDocumentContainer container, DocState state);
    DocState DocumentEnd(IDocumentContainer container, DocState state);
}