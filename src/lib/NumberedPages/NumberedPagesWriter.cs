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

                    for (var x = 0; x < _model.NumberedPages.PageCount; x++)
                        table.Cell().ColumnSpan(1)
                            .SectionLink($"NumberedPages-{x}") // link to page
                            .Section($"NumberedPagesContent-{x}") // target for link back
                            .Element(CellStyle)
                            .Text("\u279e").FontSize(42F)
                            ;

                    IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Border(1)
                            .Padding(10);
                    }
                });

            page.Footer()
                .PaddingTop(50F)
                ;
        });

        return state;
    }

    public DocState DocumentEnd(IDocumentContainer container, DocState state)
    {
        var fontFamily = Fonts.TimesNewRoman;
        var fontColor = Colors.Black;

        void PageHeader(PageDescriptor page, int index)
        {
            page.Header()
                .Section($"NumberedPages-{index}")
                .SectionLink($"NumberedPagesContent-{index}")
                .PaddingTop(50F)
                .PaddingLeft(20F)
                .Text($"Page {index + 1}")
                .AlignStart()
                .FontSize(80)
                .FontFamily(fontFamily)
                .FontColor(fontColor);
        }

        void OnePage(PageDescriptor page, int index)
        {
            var pageSize = DocUtil.GetPageSize(_model);
            page.Size(pageSize);

            page.PageColor(Colors.White);

            PageHeader(page, index);

            page.Content()
                .Column(col =>
                {
                    for (var x = 0; x < LineCount(); x++)
                        col.Item()
                            .PaddingVertical(LineSpacing())
                            .LineHorizontal(2)
                            .LineColor(Colors.Black);
                });

            page.Footer()
                .PaddingTop(50F)
                ;
        }

        for (var x = 0; x < _model.NumberedPages.PageCount; x++) container.Page(page => OnePage(page, x));

        return state;
    }

    private int LineCount()
    {
        return _model.NumberedPages.LinesPerPage;
    }

    private int LineSpacing()
    {
        return 31;
    }
}