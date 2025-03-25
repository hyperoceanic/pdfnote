using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFNote
{
    public static class DocUtil
    {
        public static PageSize GetPageSize(DocumentModel model)
        {
            PageSize result = model.Reader switch
            {
                Reader.RemarkablePaperPro => model.Orientation == Orientation.Portrait
                    ? new(1620.0F, 2160.0F, Unit.Point)
                    : new(2160.0F, 1620.0F, Unit.Point),
                Reader.Remarkable2 => model.Orientation == Orientation.Portrait
                    ? new(1404.0F, 1872.0F, Unit.Point)
                    : new(1872.0F, 1404.0F, Unit.Point),
                _ => new(1620.0F, 2160.0F, Unit.Point)
            };

            return result;
        }

        public static int DPI(DocumentModel model)
        {
            return model.Reader switch
            {
                Reader.RemarkablePaperPro => 229,
                Reader.Remarkable2 => 226,
                _ => 229
            };
        }

        public static QuestPDF.Infrastructure.Color GetColor(string colorHex)
        {
            if (!string.IsNullOrEmpty(colorHex))
            {
                return QuestPDF.Infrastructure.Color.FromHex(colorHex);
            }

            return QuestPDF.Helpers.Colors.White;
        }
    }
}