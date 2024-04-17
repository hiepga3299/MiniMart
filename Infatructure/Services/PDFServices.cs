using DinkToPdf;
using DinkToPdf.Contracts;
using PaperKind = DinkToPdf.PaperKind;

namespace MiniMart.Infatructure.Services
{
    public class PDFServices : IPDFServices
    {
        private readonly IConverter _conventor;

        public PDFServices(IConverter conventor)
        {
            _conventor = conventor;
        }
        public byte[] GeneratePDF(string contentHTML, Orientation orientation = Orientation.Landscape, PaperKind paperKind = PaperKind.A4)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperKind,
                Margins = new MarginSettings() { Top = 10 },
            };
            var objectSetting = new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = contentHTML,
                WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSetting },
            };
            return _conventor.Convert(pdf);
        }
    }
}
