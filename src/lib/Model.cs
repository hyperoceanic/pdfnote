using System.Drawing;
using System.Runtime.CompilerServices;

namespace PDFNote
{
    public enum Orientation
    {
        Portrait,
        Landscape
    }

    public enum Reader
    {
        Remarkable2,
        RemarkablePaperPro
    }

    public class DocumentModel
    {
        public string FileName { get; set; }
        public string Title { get; set; }

        public Reader Reader { get; set; }
        public Orientation Orientation { get; set; }
        public CoverModel Cover { get; set; }

        public IndexedPagesModel IndexedPages { get; set; }
    }

    public class CoverModel
    {
        public string Header { get; set; }
        public String Symbol { get; set; }
        public String Color { get; set; }
    }

    public class IndexedPagesModel
    {
        public int PageCount { get; set; }
    }
}