using System.Runtime.InteropServices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFNote
{

    public class DocCreator
    {
        DocumentModel model;
        PDFNote.Covers.CoverWriter coverWriter;
        public DocCreator()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private PDFNote.Covers.CoverWriter GetCoverWriter(DocumentModel model)
        {
            return new PDFNote.Covers.DefaultCoverWriter();
        }
        private void CreateCoverPage(IDocumentContainer container)
        {
            container.Page(page =>
            {
                string fontFamily = Fonts.TimesNewRoman;
                Color fontColor = Colors.White;

                page.Size(DocUtil.GetPageSize(model));
                page.PageColor(DocUtil.GetColor(model.Cover.Color));

                if (!String.IsNullOrEmpty(model.Cover.Header))
                {
                    coverWriter.WriteCoverHeader(page, fontFamily, fontColor, model.Cover.Header);
                }

                page.Content()
                    .PaddingVertical(5, Unit.Point)
                    .PaddingHorizontal(5, Unit.Point)
                    .Column(column =>
                        {
                            if (!String.IsNullOrEmpty(model.Cover.Symbol))
                            {
                                coverWriter.WriteCoverSymbol(column, fontFamily, fontColor, model.Cover.Symbol);
                            }

                            if (!String.IsNullOrEmpty(model.Title))
                            {
                                coverWriter.WriteCoverTitle(column, fontFamily, fontColor, model.Title);
                            }
                        });

            });

        }

        public void Create(DocumentModel model)
        {
            this.model = model;
            this.coverWriter = GetCoverWriter(model);


            Console.WriteLine($"Creating {model.FileName}");
            var result = Document.Create(container =>
            {
                CreateCoverPage(container);
            })
            .WithSettings(new DocumentSettings
            {
                PdfA = false,
                CompressDocument = true,
                ImageCompressionQuality = ImageCompressionQuality.High,
                ImageRasterDpi = DocUtil.DPI(model),
                ContentDirection = ContentDirection.LeftToRight
            });

            result.GeneratePdf(model.FileName);
        }
    }
}