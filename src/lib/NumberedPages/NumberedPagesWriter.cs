using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFNote.Pages;

public class NumberedPagesWriter : IPageSetWriter
{
    private readonly DocumentModel _model;

    public NumberedPagesWriter(DocumentModel model)
    {
        _model = model;
    }

    public DocState DocumentStart(IDocumentContainer container, DocState state)
    {
        container.Page(page =>
        {
            var fontFamily = Fonts.TimesNewRoman;
            var fontColor = Colors.Black;

            page.Size(DocUtil.GetPageSize(_model));
            page.PageColor(Colors.White);

            page.Header()
                .PaddingTop(100F)
                .PaddingLeft(100F)
                .Text("Contents")
                .AlignStart()
                .FontSize(96)
                .FontFamily(fontFamily)
                .FontColor(fontColor);

            page.Content()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns => { columns.RelativeColumn(); });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle); //.Text("Contents");

                        IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Background(DocUtil.GetColor(_model.Cover.Color))
                                .PaddingVertical(8)
                                .PaddingHorizontal(16);
                        }
                    });

                    table.Cell().ColumnSpan(1)
                        .Background(Colors.Grey.Lighten2).Element(CellStyle)
                        .Text("\u279e").FontSize(42F);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.Border(1).Padding(10);
                    }
                });
        });

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
            // var fontFamily = Fonts.TimesNewRoman;
            // var fontColor = Colors.Black;
            //
            // page.Size(DocUtil.GetPageSize(model));
            // page.PageColor(DocUtil.GetColor(model.Cover.Color));
            //
            // page.Header()
            //     .PaddingTop(100F)
            //     .PaddingLeft(100F)
            //     .Text("Contents")
            //     .AlignStart()
            //     .FontSize(96)
            //     .FontFamily(fontFamily)
            //     .FontColor(fontColor);

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