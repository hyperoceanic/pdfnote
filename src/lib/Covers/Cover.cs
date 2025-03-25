namespace PDFNote.Covers
{
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public abstract class CoverWriter
    {
        public abstract void WriteCoverHeader(PageDescriptor page, string fontFamily, Color fontColor, string header);
        public abstract void WriteCoverSymbol(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol);

        public abstract void WriteCoverTitle(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol);

    }
}