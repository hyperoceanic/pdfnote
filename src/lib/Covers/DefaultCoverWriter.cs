using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFNote.Pages;

public class DefaultCoverWriter : IPageSetWriter
{
    private readonly DocumentModel model;

    public DefaultCoverWriter(DocumentModel model)
    {
        this.model = model;
    }

    public DocState DocumentStart(IDocumentContainer container, DocState state)
    {
        container.Page(page =>
        {
            var fontFamily = Fonts.TimesNewRoman;
            var fontColor = Colors.White;

            page.Size(DocUtil.GetPageSize(model));
            page.PageColor(DocUtil.GetColor(model.Cover.Color));

            if (!string.IsNullOrEmpty(model.Cover.Header))
                WriteCoverHeader(page, fontFamily, fontColor, model.Cover.Header);

            page.Content()
                .PaddingVertical(5)
                .PaddingHorizontal(5)
                .Column(column =>
                {
                    if (!string.IsNullOrEmpty(model.Cover.Symbol))
                        WriteCoverSymbol(column, fontFamily, fontColor, model.Cover.Symbol);

                    if (!string.IsNullOrEmpty(model.Title)) WriteCoverTitle(column, fontFamily, fontColor, model.Title);
                });
        });
        return state;
    }

    public DocState DocumentEnd(IDocumentContainer container, DocState state)
    {
        return state;
    }

    private void WriteCoverHeader(PageDescriptor page, string fontFamily, Color fontColor, string header)
    {
        page.Header()
            .PaddingTop(100F)
            .PaddingLeft(100F)
            .Text(header)
            .AlignStart()
            .FontSize(96)
            .FontFamily(fontFamily)
            .FontColor(fontColor);
    }

    private void WriteCoverSymbol(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol)
    {
        column.Item()
            .PaddingTop(150F)
            .AlignCenter()
            .AlignMiddle()
            .Text(symbol)
            .FontColor(fontColor)
            .FontFamily(fontFamily)
            .FontSize(720);
    }

    private void WriteCoverTitle(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol)
    {
        column.Item()
            .PaddingLeft(120)
            .ExtendVertical()
            .AlignBottom()
            .AlignLeft()
            .PaddingBottom(100F)
            .PaddingVertical(6F)
            .Text(symbol)
            .FontColor(fontColor)
            .FontFamily(fontFamily)
            .FontSize(256);
    }
}