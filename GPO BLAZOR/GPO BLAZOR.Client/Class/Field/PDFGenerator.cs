
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using Microsoft.AspNetCore.Components.RenderTree;
using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace GPO_BLAZOR.Client.Class.Field;
public class PdfGenerator
{
    public static async Task<byte[]> CreatePdf()
    {
        using (var memoryStream = new MemoryStream())
        {
            if (Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Generated with PDFsharp";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            var width = page.Width;
            var height = page.Height;
            gfx.DrawLine(XPens.Red, 0, 0, width, height);
            gfx.DrawLine(XPens.Red, width, 0, 0, height);

            var radius = width / 5;
            gfx.DrawEllipse(new XPen(XColors.Red, 1.5), XBrushes.White, new XRect(width / 2 - radius, height / 2 - radius, 2 * radius, 2 * radius));

            XFont font = new XFont("Times New Roman", 20, XFontStyleEx.Regular);
            gfx.DrawString("Hello, PDFsharp!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            document.Save(memoryStream);
            var doc = await PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().Render();
            PdfDocumentRenderer renderer = new PdfDocumentRenderer();
            Section section = doc.AddSection();
            section.AddParagraph("2345");

            // Render MigraDoc content
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save("output.pdf");

            return memoryStream.ToArray();
        }
    }
}