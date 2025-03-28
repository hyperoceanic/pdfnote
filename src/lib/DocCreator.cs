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
            return new PDFNote.Covers.DefaultCoverWriter(model);
        }

        private void CreateIndexedPages(IDocumentContainer container)
        {

        }
        public void Create(DocumentModel model)
        {
            this.model = model;

            Console.WriteLine($"Creating {model.FileName}");
            var result = Document.Create(container =>
            {
                if (model.Cover != null)
                {
                    this.coverWriter = GetCoverWriter(model);
                    this.coverWriter.CreateCoverPage(container);
                }

                if (model.IndexedPages != null)
                {
                    CreateIndexedPages(container);
                }
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