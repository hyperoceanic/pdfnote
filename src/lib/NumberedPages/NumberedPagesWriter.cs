using PDFNote.Pages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFNote.Covers;

public abstract class NumberedPagesWriter : IPageSetWriter
{
    private readonly DocumentModel model;

    public NumberedPagesWriter(DocumentModel model)
    {
        this.model = model;
    }

    public DocState DocumentStart(IDocumentContainer container, DocState state)
    {
        return state;
    }

    public DocState DocumentEnd(IDocumentContainer container, DocState state)
    {
        return state;
    }

    public void CreateIndexPages(IDocumentContainer container)
    {
        container.Page(page =>
        {
            var fontFamily = Fonts.TimesNewRoman;
            var fontColor = Colors.White;

            page.Size(DocUtil.GetPageSize(model));
            page.PageColor(DocUtil.GetColor(model.Cover.Color));

            // if (!string.IsNullOrEmpty(model.Cover.Header))
            //     WriteCoverHeader(page, fontFamily, fontColor, model.Cover.Header);
            //
            // page.Content()
            //     .PaddingVertical(5)
            //     .PaddingHorizontal(5)
            //     .Column(column =>
            //     {
            //         if (!string.IsNullOrEmpty(model.Cover.Symbol))
            //             WriteCoverSymbol(column, fontFamily, fontColor, model.Cover.Symbol);
            //
            //         if (!string.IsNullOrEmpty(model.Title)) WriteCoverTitle(column, fontFamily, fontColor, model.Title);
            //     });
        });
    }
}