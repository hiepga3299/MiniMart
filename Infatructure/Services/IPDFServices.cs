using DinkToPdf;

namespace MiniMart.Infatructure.Services
{
    public interface IPDFServices
    {
        byte[] GeneratePDF(string contentHTML, Orientation orientation = Orientation.Landscape, PaperKind paperKind = PaperKind.A4);
    }
}