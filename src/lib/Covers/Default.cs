namespace PDFNote.Covers
{
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class DefaultCoverWriter : CoverWriter
    {
        public DefaultCoverWriter(DocumentModel model) : base(model) { }
        public override void WriteCoverHeader(PageDescriptor page, string fontFamily, Color fontColor, string header)
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

        public override void WriteCoverSymbol(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol)
        {
            column.Item()
                .PaddingTop(100F)
                .AlignCenter()
                .AlignMiddle()
                .Text(symbol)
                .FontColor(fontColor)
                .FontFamily(fontFamily)
                .FontSize(720);
        }

        public override void WriteCoverTitle(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol)
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
}