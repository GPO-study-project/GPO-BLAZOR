using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using MigraDoc.RtfRendering;
using PdfSharp;
using System.Xml.Serialization;


namespace GPO_BLAZOR.Client.Class.Field
{
    struct GetSet
    {
        public delegate string Getter(string Name);
        public delegate void Setter(string Name, string Value);
        private Func<string>? _getter;
        private Action<string>? _setter;


        public event Func<string> AddGet
        {
            add
            {
                _getter += value;
            }
            remove
            {
                if (_getter != null)
                    _getter -= value;
            }
        }

        public event Action<string> AddSet
        {
            add
            {
                _setter += value;
            }
            remove
            {
                if (_setter is not null)
                    _setter -= value;
            }
        }

        public string Get()
        {
            if (_getter is not null)
                return _getter();
            return "";
        }

        public void Set(string Value)
        {
            if (_setter != null)
                _setter(Value);
        }

    }
    public partial class Print: ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Parameter]
        //[EditorRequired]
        public string TemplateName { get; set; }

        //[EditorRequired]
        [Parameter]
        public IStatmen Statmen { get; set; }       

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new PdfFilePrinting.DocumentService.MyFontResolver();
            base.OnAfterRenderAsync(firstRender);
            //ReadXml("");
        }


        protected void ButtonCliced()
        {

        }
        private async void ReadXml()
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PdfFilePrinting.DocumentService.Document));
            var reques = await Date.Requesting.AutorizationedRequest(new Uri($"https://{IPaddress.IPAddress}/GetPrintAtribute/{TemplateName}"), JSRuntime);
            var result = xmlSerializer.Deserialize(reques) as PdfFilePrinting.DocumentService.Document?;

            PdfDocumentRenderer pdfDocumentRenderer = new PdfDocumentRenderer();
            try
            {
                var structures = Statmen.Date.SelectMany(x => x.Date).SelectMany(y => y.Date).Select(z => new KeyValuePair<string, string>(z.Name, z.value));
                var doc = result.Value;
                var Names = doc.GetNames().GroupBy(x => x.Name).Select(x => new KeyValuePair<string, GetSet>(x.Key, x.Aggregate(new GetSet(), (a, b) =>
                {
                    a.AddGet += b.getter;
                    a.AddSet += b.setter;
                    return a;
                }))).ToDictionary();
                foreach (var temp in structures)
                {
                    if (Names.ContainsKey(temp.Key))
                    {
#if DEBUG
                        Console.WriteLine($"Keys Pair :{temp.Key} : {temp.Value}");
#endif
                        Names[temp.Key].Set(temp.Value);
                    }
                }
                try
                {
                    pdfDocumentRenderer.Document = doc.Render();
                    //pdfDocumentRenderer.Document = pdfrend;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error printPdf3 -> " + ex.Message);
                }
                try
                {
                    pdfDocumentRenderer.RenderDocument();
                    //pdfDocumentRenderer.Document = pdfrend;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error printPdf4 -> " + ex.Message);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error print -> " + ex.Message);
            }
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    pdfDocumentRenderer.PdfDocument.Save(memoryStream);
                    await JSRuntime.InvokeVoidAsync("saveAsFile", "fileName.pdf", Convert.ToBase64String(memoryStream.ToArray()));
                    RtfDocumentRenderer h = new RtfDocumentRenderer();
                    h.Render(result.Value.Render(), memoryStream, "/");
                    await JSRuntime.InvokeVoidAsync("saveAsFile", "fileName.rtf", Convert.ToBase64String(memoryStream.ToArray()));

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error print2 -> " + ex.Message);
            }
            //var url = $"data:application/pdf;base64,{base64}";
            //await JSRutime.InvokeVoidAsync("open", url, "_blank");
        }

    }

   
}
