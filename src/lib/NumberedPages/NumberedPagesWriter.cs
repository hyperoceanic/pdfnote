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
                .PaddingTop(80F)
                .PaddingBottom(20F)
                .PaddingLeft(20F)
                .Text(_model.Title)
                .AlignCenter()
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
                            .Section($"NumberedPagesContent-{x}") // target for link back
                            .Element(CellStyle)
                            .SectionLink($"NumberedPages-{x}") // link to page
                            .Text($"{x + 1}").FontSize(42F)
                            ;

                    IContainer CellStyle(IContainer container)
                    {
                        if (_model.Handedness == Handedness.Right)
                            return container
                                .Border(1F)
                                .AlignRight()
                                .Padding(20);
                        return container
                            .Border(1)
                            .AlignLeft()
                            .Padding(10);
                    }
                });

            page.Footer()
                .PaddingTop(10F)
                .PaddingBottom(50F)
                .AlignCenter()
                .Text(_model.Cover.Header)
                .AlignCenter()
                .FontSize(40)
                .FontFamily(fontFamily)
                .FontColor(fontColor);
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
                .Height(100F)
                .Element(CellStyle)
                .SectionLink($"NumberedPagesContent-{index}")
                .Text($"Page {index + 1}")
                .AlignLeft()
                .FontSize(40)
                .FontFamily(fontFamily)
                .FontColor(Color.FromHex("#d3d3d3"));
            ;

            IContainer CellStyle(IContainer container)
            {
                if (_model.Handedness == Handedness.Right)
                    return container
                     //   .Border(1F)
                        .AlignRight()
                        .Padding(20);
                return container
                   // .Border(1F)
                    .AlignLeft()
                    .Padding(10);
            }
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
                            .AlignBottom()
                            .LineHorizontal(2)
                            .LineColor(Colors.Grey.Lighten1);
                });

            page.Footer()
                .PaddingTop(10F)
                .PaddingBottom(80F)
                .AlignCenter()
                .SectionLink($"NumberedPagesContent-{index}")
                .Text($"{_model.Title} - Page {index + 1}")
                .AlignCenter()
                .FontSize(40)
                .FontFamily(fontFamily)
                .FontColor(fontColor);
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