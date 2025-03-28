namespace PDFNote.Covers
{
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public abstract class CoverWriter
    {
        DocumentModel model;
        public CoverWriter(DocumentModel model)
        {
            this.model = model;
        }

        public abstract void WriteCoverHeader(PageDescriptor page, string fontFamily, Color fontColor, string header);
        public abstract void WriteCoverSymbol(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol);

        public abstract void WriteCoverTitle(ColumnDescriptor column, string fontFamily, Color fontColor, string symbol);

        public void CreateCoverPage(IDocumentContainer container)
        {
        
            container.Page(page =>
            {
                string fontFamily = Fonts.TimesNewRoman;
                Color fontColor = Colors.White;

                page.Size(DocUtil.GetPageSize(model));
                page.PageColor(DocUtil.GetColor(model.Cover.Color));

                if (!String.IsNullOrEmpty(model.Cover.Header))
                {
                    WriteCoverHeader(page, fontFamily, fontColor, model.Cover.Header);
                }

                page.Content()
                    .PaddingVertical(5, Unit.Point)
                    .PaddingHorizontal(5, Unit.Point)
                    .Column(column =>
                        {
                            if (!String.IsNullOrEmpty(model.Cover.Symbol))
                            {
                                WriteCoverSymbol(column, fontFamily, fontColor, model.Cover.Symbol);
                            }

                            if (!String.IsNullOrEmpty(model.Title))
                            {
                                WriteCoverTitle(column, fontFamily, fontColor, model.Title);
                            }
                        });
            });

        }
    }
}