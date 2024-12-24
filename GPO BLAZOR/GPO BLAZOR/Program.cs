using GPO_BLAZOR.Components;
using Microsoft.AspNetCore.Components.Authorization;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using GPO_BLAZOR.API_Functions;
using DBAgent;
using StatmenDate = GPO_BLAZOR.FiledConfiguration.StatmenDate;
using Document = PdfFilePrinting.DocumentService.Document;
using MigraDoc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;
using MigraDoc.RtfRendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MigraDoc.DocumentObjectModel;
using GPO_BLAZOR.Client.Class.Date;
using GPO_BLAZOR.FiledConfiguration.Document;
using DBAgent.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections;
using System;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using PdfFilePrinting.DocumentService;
using static Azure.Core.HttpHeader;




namespace GPO_BLAZOR
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


    class Date
    {
        public Guid token { get; set; }
        public string jwt { get; set; }
        public string[] role { get; set; }
    }

    /*record FieldDateContainer
    {

        public FieldDateContainer (string name, string type, string id, string text, bool disabled)
        {
            Name = name;
            ClassType = type;
            Id = id;
            Text = text;
            IsDisabled = disabled;
        }

        public string Name { get; set; }
        public string ClassType { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }

        public bool IsDisabled { get; init; }

    }

    class BlockDateContainer
    {
        public BlockDateContainer(string blockname, IEnumerable<FieldDateContainer> date)
        {
            BlockName = blockname;
            Date = date.ToArray();
        }

        public string BlockName { get; set; }
        public FieldDateContainer[] Date { get; set; }
    }

    class PageDateContainer
    {
        public PageDateContainer(string pagename, IEnumerable<BlockDateContainer> date)
        {
            PageName = pagename;
            Date = date.ToArray();
        }

        public string PageName { get; set; }
        public BlockDateContainer[] Date { get; set; }
    }

    class StatmenDateContainer
    {
        public StatmenDateContainer(IEnumerable<PageDateContainer> date)
        {
            //StatmenName = statmenname;
            Date = date.ToArray();
        }

        //public string StatmenName { get; set; }
        public PageDateContainer[] Date { get; set; }
    }*/

    

    class tempc
    {
        public tempc(string id, string Time, int val1, int val2)
        {
            this.id = id;
            this.Time = DateTime.Now.AddHours(2);
            this.State = val1;
            this.PracticType = val2;
        }

        public string id { get; set; }
        public DateTime Time { get; set; }
        public int State { get; set; }
        public int PracticType { get; set; }
    }

    
    record AutorizationDate
    {
        public string login { get; init; }
        public string Password { get; init; }
    }

    class ErrorMessage
    {
        public string messege { get; set; }
    }

    /// <summary>
    /// Èñïðàâèòü
    /// </summary>
    public static class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // èçäàòåëü òîêåíà
        public const string AUDIENCE = "MyAuthClient"; // ïîòðåáèòåëü òîêåíà
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // êëþ÷ äëÿ øèôðàöèè
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
    //

#region testingPdfextension
    public static class PdfSharpExtensions
    {
        public static IEnumerable<string> ExtractText(this PdfPage page)
        {
            var content = ContentReader.ReadContent(page);
            var text = content.ExtractText();
            return text;
        }

        public static IEnumerable<string> ExtractText(this CObject cObject)
        {
            switch (cObject)
            {
                case COperator cOperator:
                    if (cOperator.OpCode.OpCodeName == OpCodeName.Tj ||
                    cOperator.OpCode.OpCodeName == OpCodeName.TJ)
                    {
                        foreach (var cOperand in cOperator.Operands)
                            foreach (var txt in ExtractText(cOperand))
                                yield return txt;
                    }
                    else
                    {
                        foreach (var cOperand in cOperator.Operands)
                            foreach (var txt in ExtractText(cOperand))
                                yield return txt+" - ethertype - "+ cOperator.OpCode.Name;
                    }
                    
                    break;
                case CSequence cSequence:
                    foreach (var element in cSequence)
                        foreach (var txt in ExtractText(element))
                            yield return txt;
                    break;
                case CString cString:
                    var type = cString.CStringType;
                    yield return cString.Value;
                    break;
            }
        }
    }
#endregion

    public class Program
    {
        static Dictionary<string, List<string>> SpecialArray = new Dictionary<string, List<string>>()
        {
            {"Grp", new List<string>() {"711-1", "721-1", "731-1", "731-2", "741-1", "761-1" } } ,
            {"Direction", new List<string>(){"Èíôîðìàöèîííàÿ áåçîïàñíîñòü", "Áåçîïàñíîñòü àâòîìàòèçèðîâàííûõ ñèñòåì", "Áåçîïàñíîñòü òåëåêîìóíèêàöèîííûõ ñèñòåì", "Àíàëèòè÷åñêàÿ áåçîïàñíîñòü", "Ýêîíîìè÷åñêàÿ áåçîïàñíîñòü" } },
            {"PracticeSort", new List<string>(){"Ïðîèçâîäñòâåííàÿ", "Ïðåääèïëîìàíàÿ" } },
            {"PracticeType", new List<string>(){"Ýêñïëóàòàöèîííàÿ"} },
            {"Postlist", new List<string>(){"based", "post", "contract" } },
            {"",  new List<string>(){"based", "post", "contract"}}
        };

        private static string EncodingHack(string str)
        {
            var encoding = Encoding.BigEndianUnicode;
            var bytes = encoding.GetBytes(str);
            var sb = new StringBuilder();
            sb.Append((char)254);
            sb.Append((char)255);
            for (int i = 0; i < bytes.Length; ++i)
            {
                sb.Append((char)bytes[i]);
            }
            return sb.ToString();
        }

        public static void Main(string[] args)
        {


            /*{
                var tyop = new PdfDocumentRenderer();

                /*FileStream fstreams = new FileStream("./file1.pdf", FileMode.Open);
                byte[] bufchar = new byte[fstreams.Length];

                fstreams.Read(bufchar, 0, bufchar.Length);
                StreamReader rtg = new StreamReader(fstreams);

                Console.WriteLine(Encoding.UTF8.GetString(bufchar));

                

                    using (MemoryStream memoryStream = new MemoryStream(bufchar))
                    {
                        using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                        {
                            
                            using (StreamReader streamReader = new StreamReader(gzipStream))
                            {
                                Console.WriteLine( streamReader.ReadToEnd());
                            }
                        }
                        //(Encoding.Default.GetString(memoryStream.ToArray())); 
                    }
               
                *//*
            tyop.PdfDocument = PdfSharp.Pdf.IO.PdfReader.Open("./Pdf5.pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.ReadOnly);

                PdfPage SamplePage = tyop.PdfDocument.Pages[0];
                PdfDictionary.PdfStream stream = SamplePage.Contents.Elements.GetDictionary(0).Stream;
                
                var content = ContentReader.ReadContent(SamplePage);
                var text = PdfSharpExtensions.ExtractText(content).ToArray();
                foreach (var lol in text)
                    Console.WriteLine(lol);

                CObject tyui = ContentReader.ReadContent(tyop.PdfDocument.Pages[0]);
                var textingpdf = tyui.ExtractText().ToArray();

                
                var rustran = EncodingHack("Эксплуатационной практики");

                    foreach (var itmj in textingpdf)
                        Console.WriteLine(itmj);

                Uri uri = new Uri("https://egrul.nalog.ru");

                HttpClient ugrn1 = new HttpClient();

                HttpMessageHandler ugrn2 = new HttpClientHandler();


                FileStream str = new FileStream("person.json", FileMode.OpenOrCreate);
                var optionsed = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    AllowTrailingCommas = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                };
                var hjobj = PdfFilePrinting.MakeTemplate.MakeAskFormTemplate.Make();
                var JSONSer = JsonSerializer.Serialize(hjobj, optionsed);
                byte[] inputBuffer = Encoding.Default.GetBytes(JSONSer);

                str.Write(inputBuffer, 0, inputBuffer.Length);
                str.Close();*/


                /*XmlSerializer xmlSerializered = new(typeof(Document));
                FileStream str = new FileStream("person.xml", FileMode.Create);

            // ïîëó÷àåì ïîòîê, êóäà áóäåì çàïèñûâàòü ñåðèàëèçîâàííûé îáúåêò
                var rest = PdfFilePrinting.MakeTemplate.MakeAskFormTemplate.Make();

                xmlSerializered.Serialize(str, rest);*/


                /*var rtfRender = new RtfDocumentRenderer();
                rtfRender.Render(PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().Render(), "Contract.rtf", "./");




                Console.WriteLine("\nObject has been serialized\n");
                var temp2 = PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().Render();

                str.Close();
                str = new FileStream("person.xml", FileMode.Open);
                Document? res = xmlSerializered.Deserialize(str) as Document?;
            Document Res1 = PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make();
            Document Res2 = res.Value;
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = Res2.Render();
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save("PDFFile3.pdf");

            //TestPrinter.F(new FileStream("./file123.pdf", FileMode.OpenOrCreate));
            str.Close();
            }
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

        };*/
            //TestPrinter.F(new FileStream("./file123.pdf", FileMode.OpenOrCreate));
/*
            FileStream fstream = new FileStream("person.xml", FileMode.Open);
            byte[] buffer = new byte[fstream.Length];
            fstream.ReadExactly(buffer);
            string textFromFile = Encoding.Default.GetString(buffer);

            #region Create Accesor
            var CustomDoc = PdfFilePrinting.MakeTemplate.MakeAskFormTemplate.Make();
            var h1 = CustomDoc.GetNames().GroupBy(x => x.Name).Select(x => new KeyValuePair<string, GetSet>(x.Key, x.Aggregate(new GetSet(), (a, b) =>
            {
                a.AddGet += b.getter;
                a.AddSet += b.setter;
                return a;
            })));
            var h2 = h1.ToDictionary();
            var tempManeM = "FactoryLeaderName";//"OrganiztionLeaderName";
            //h2[tempManeM].Set("Сергеев Сергей Сергеевич");

            var RDoc = CustomDoc.Render();
            PdfDocumentRenderer renderer = new PdfDocumentRenderer();
            renderer.Document = PdfFilePrinting.MakeTemplate.MakeAskFormTemplate.Make().Render();
            foreach (var iuy in PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().GetNames().Select(x => x.Name).Distinct())
            {
                Console.WriteLine("Result.Add(\"" + iuy + "\", );");
            }
            renderer.RenderDocument();
            var result = renderer.PdfDocument;
            result.Save("Pdf4.pdf");
            renderer = new PdfDocumentRenderer();

            renderer.Document = RDoc;
            renderer.RenderDocument();
            result = renderer.PdfDocument;
            result.Save("Pdf5.pdf");

            RtfDocumentRenderer rtfren = new RtfDocumentRenderer();
            rtfren.Render(PdfFilePrinting.MakeTemplate.MakeAskFormTemplate.Make().Render(), "Rtf4.rtf", "./");
            #endregion

            #region Create Fields
            FiledConfiguration.Document.IDocument doc = new FiledConfiguration.Document.Documnet()
            {
                Name = "Заявление",
                Description = "Something",
                Fields = CustomDoc.GetNames().Select(x => x.Name).Distinct().Select(x => (IFields)(new Fields() { Name = x }))
            };
            #endregion

            
           
            */


            var urlstr = Environment.GetEnvironmentVariable("VS_TUNNEL_URL");
            var cntyui = Environment.GetEnvironmentVariables();


            Console.WriteLine($"Envirment Tunnel URL {urlstr}");
            Console.Write("Data Base Password: ");
            var Password = Console.ReadLine();
            Gpo2Context cntx = new Gpo2Context(Password);


            var FieldsTemplate = cntx.Fields.Select(x => (FiledConfiguration.FieldCont.IField)new FiledConfiguration.FieldCont.Field()
            {
                Name = x.Name,
                Path = new FiledConfiguration.FieldCont.Path()
                {
                    Block = x.Block,
                    Page = x.Page
                },
                Template = new FiledConfiguration.FieldDateContainer(x.Name, x.Type, x.Name, x.Text, !x.Mutability),
            });

           XmlSerializer xmlSerializer = new XmlSerializer(typeof(Document));
            var DocTemplate = cntx.Templates
                .Select(x => new KeyValuePair<string, Document>(x.Name, (Document)xmlSerializer
                    .Deserialize(new StringReader(x.TemplateBody))));


            var DocFieldsTemps = DocTemplate
                .AsEnumerable()
                .Select(x => new KeyValuePair<string, IEnumerable<string>>
                (
                    x.Key,
                    x.Value
                        .GetNames()
                        .Append((Name: "Commentary", getter: async ()=>"", setter: async (string x) => { }
                ))
                        .Select(y => y.Name)
                        .Distinct()
                ));

            var DocFields = DocFieldsTemps
                .Select(x => (IDocument)new FiledConfiguration.Document.Documnet()
                {
                    Name = x.Key,
                    Fields = x.Value
                        .Select(y => (IFields)new Fields() { Name = y })
                });

            IDictionary<string, FiledConfiguration.FieldCont.IField> FiledResult;


            var DocFieldsToList = DocFields;

            var RequestTemplates = FiledConfiguration.Constructor.GetFields(FieldsTemplate, DocFieldsToList, out FiledResult);


            // DBConnector.F(null);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<CookieStorageAccessor>();
            builder.Services.AddScoped<LocalStorageAccessor>();
            builder.Services.AddSingleton<IAutorizationStruct, AutorizationStruct>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddControllers()
                .AddXmlSerializerFormatters();



            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", opt =>
                {
                    opt.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                        //.AllowCredentials();
                });
            });

            builder.Services.AddScoped<AuthenticationStateProvider, IdentetyAuthenticationStateProvider>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });


            string connection = builder.Configuration.GetConnectionString("DefaultConnection") + Password;
            builder.Services.AddDbContext<Gpo2Context>(options => options.UseNpgsql(connection));
            //builder.Services.AddAuthorizationCore();

            var app = builder.Build();
            app.UseRouting();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            List<tempc> b = new List<tempc>()
            {
                new tempc(1.ToString(), 1.ToString(), 1, 1),
                new tempc(2.ToString(), 2.ToString(), 1, 1),
                new tempc(3.ToString(), 3.ToString(), 1, 1)
            };

            Dictionary<int, Dictionary<string, string>> temp = new Dictionary<int, Dictionary<string, string>>()
            {
                {1, new Dictionary<string, string>() { {"id", "1" }, {"Template", "based" } } },
                {2, new Dictionary<string, string>() { {"id", "2" }, { "Template", "based" } } },
                {3, new Dictionary<string, string>() { {"id", "3" }, { "Template", "based" } } }
            };

            Dictionary<string, string> PrintTemplate = null;


            bool AddOnDictionary<Key, Value>(Dictionary<Key, Value> dictionary, KeyValuePair<Key, Value> value)
            {
                if (dictionary.TryAdd(value.Key, value.Value))
                    return true;
                else
                    dictionary[value.Key] = value.Value;
                return true;
            }

            int calculator = 0;

            var (first, second) = GPO_BLAZOR.FiledConfiguration.StatmenDate.ExperementalTemplate();

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAntiforgery();
            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.MapGet("/CaseWaord", async (PdfFilePrinting.DocumentService.WordCase wordCase, string Name) =>
                {
                    if (Name is null)
                    {
                        return Name;
                    }
                    HttpClient htpc = new HttpClient();
                    htpc.BaseAddress = new Uri("https://surnameonline.ru");
                    htpc.DefaultRequestHeaders.Host = "surnameonline.ru";
                    htpc.DefaultRequestHeaders.Add("Origin", new[] { "https://surnameonline.ru" });
                    htpc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    htpc.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("UTF8"));
                    //htpc.DefaultRequestHeaders.Add("Content-Type", new[] { , "charset=UTF-8" });
                    htpc.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    htpc.DefaultRequestHeaders.Add("mode", "no-cors");
                    htpc.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");

                    htpc.DefaultRequestHeaders.Referrer = new Uri("https://surnameonline.ru");
                    //htpc.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    htpc.BaseAddress = new Uri("https://surnameonline.ru/");
                    string[] Values = Name.Split(' ');
                    if (Values.Length < 3)
                    { Values = new string[3] { "Иван", "Иванов", "Иванович" }; };
                    string responce = $"name={Values[1]}&surname={Values[0]}&patronymic={Values[2]}";
#if DEBUG
                    Console.WriteLine(responce);
#endif
                    var result = await htpc.PostAsync("/inflect.php", new StringContent(responce,
                                                        Encoding.UTF8,
                                                        "application/x-www-form-urlencoded"));


                    var result3 = await result.Content.ReadAsStringAsync();


                    XDocument xdoc = XDocument.Parse($"<Document>{result3}</Document>");
                    var xelements = xdoc.Element("Document").Element("ul").Elements("li").ToArray();
                    var resultxml = xelements.Select(x => (x.Value.Substring(4))).ToArray();
                    return resultxml[(int)wordCase];
                });

            ///<summary>
            ///Выдача атрибутов для поля
            ///</summary>
            app.MapGet("/GetAtributes/{Field}", [Authorize] async (string? ID, string Field, HttpContext context, Gpo2Context cntx) =>
            {
                try
                {

                    var Identity = context.User.Identity;
                    string username;
                    if (Identity == null)
                        return Results.NoContent();
                    else
                        username = Identity.Name;

                    var role = context.User.Claims.First(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Split('\n');
                    if (!role.Contains("Student"))
                    {
                        var t1 = await cntx.AskForms.FirstOrDefaultAsync(x => x.Id.ToString() == ID);
                        var t2 = await cntx.Students.FindAsync(t1.Student);
                        var t3 = await cntx.Users.FindAsync(t2.User);
                        username = t3.Email;
                    }


                    ///<summary>
                    ///Получения списка доступных для создания документов
                    ///</summary>
                    switch (Field)
                    {
                        case "Postlist":

                            var UserInDB = await cntx.Users.AsNoTracking().Where(x => x.Email == username).FirstOrDefaultAsync();
                            var AskForms = cntx.AskForms.AsNoTracking().Where(x => x.Student == UserInDB.Id);
                            if ((await AskForms.FirstOrDefaultAsync()) is null)
                            {
                                return Results.Json(new string[] { "Заявление" });
                            }
                            else
                            {
                                var SingleForm = await AskForms.FirstOrDefaultAsync();
                                var contracts = await cntx.Contracts.AsNoTracking().Where(x => x.AskForms.Contains(SingleForm)).FirstOrDefaultAsync();
                                if (contracts is not null)
                                {
                                    return Results.Json(new string[] { "Заявление", "Договор" });
                                }
                                else
                                {
                                    return Results.Json(new string[] { "Иное" });
                                }
                            }

                        case "Cafedral":
                            return Results
                                .Json(cntx.Students
                                    .Include(x => x.UserNavigation)
                                    .Where(x => x.UserNavigation.Email == username)
                                    .Select(z => z.GroupNavigation.CafedralNavigation.EncriptedName)
                                    .ToArray());
                        case "Cafedral Leader":
                            return Results
                                .Json(cntx.Students
                                    .Include(x => x.UserNavigation)
                                    .Where(x => x.UserNavigation.Email == username)
                                    .Select(z => z.GroupNavigation.CafedralNavigation.LeaderNavigation)
                                    .Select(y => $"{y.LastName} {y.FirstName} {y.MiddleName}")
                                    .ToArray());
                        case "Practic Type":
                            return Results
                                .Json(cntx.PracticeTypes
                                    .Select(x => x.Name)
                                    .ToArray());
                        case "Practic Sort":
                            return Results
                                .Json(cntx.PracticeTypes
                                    .Select(x => x.Name)
                                    .ToArray());
                        case "DerictionType":
                            return Results
                                .Json(cntx.Students
                                    .Include(x => x.UserNavigation)
                                    .Where(x => x.UserNavigation.Email == username)
                                    .Select(z => z.GroupNavigation.DirectionNavigation.Name)
                                    .ToArray());
                        case "CafedralPracticFielderLeader":
                            return Results
                                .Json(cntx.Students
                                .Include(x => x.UserNavigation)
                                .Where(x => x.UserNavigation.Email == username)
                                .Select(z => z.GroupNavigation.DirectionNavigation.LeaderNavigation)
                                .Select(y => $"{y.LastName} {y.FirstName} {y.MiddleName}")
                                .ToArray());
                        case "Curse":
                            return Results
                                .Json(cntx.Students
                                .Include(x => x.UserNavigation)
                                .Where(x => x.UserNavigation.Email == username)
                                .Select(z => z.GroupNavigation.Cours.ToString())
                                .ToArray());
                        case "Group":
                            return Results
                                .Json(cntx.Students
                                .Include(x => x.UserNavigation)
                                .Where(x => x.UserNavigation.Email == username)
                                .Select(z => z.GroupNavigation.Groups)
                                .ToArray());
                        default:
                            try
                            {
                                var responce = SpecialArray[Field];
                                return Results.Json(responce);
                            }
                            catch
                            {
                                return Results.Problem();
                            }
                    }
                }
                catch (Exception ex)
                {
                    app.Logger.LogError($"Error: {ex.Message}");
                    return Results.Problem();
                }

            });


            app.MapGet("/GetAtributes", () => Results.NotFound("Atribute"));


            ///<summary>
            ///Авторизация
            ///</summary>
            app.MapPost("/autorization", async (Autorization.AutorizationDate date, Gpo2Context cntx) =>
            {
                try
                {
                    var (UserIsNotNull, userID) = await Autorization.checkuser(date, cntx);
                    if (!UserIsNotNull)
                    {
                        app.Logger.LogWarning($"User: {date.login} Bad Autorization");
                        return Results.Problem("not login or password", "nonautorization", 401, "bad login or password)", "nontype", new Dictionary<string, object> { { "messege", "bad login or password" } });
                    }

                    app.Logger.LogInformation($"User loging: {date.login}");

                    var RoleConstructor = cntx.Users.Where(x => x.Email == date.login).Include(x => x.Рольs);

                    var Rols = RoleConstructor.FirstOrDefault().Рольs.Select(x=>x.Name).Aggregate((x,y)=>x+"\n"+y);

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, date.login),
                            new Claim(ClaimTypes.Role, Rols),
                            new Claim ("ID",userID.ToString())
                        };

                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(40)), // âðåìÿ äåéñòâèÿ 2 ìèíóòû
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));



                    return Results.Json(new Date() {
                        token = (Guid.NewGuid()),
                        jwt = new JwtSecurityTokenHandler().WriteToken(jwt),
                        role = Rols.Split('\n') 
                        
                    });
                    /*
                    return !(API_Functions.Autorization.checkuser(date, cntx).Result) ?
                    Results.Problem("not login or password", "nonautorization", 401, "bad login or password)", "nontype", new Dictionary<string, object> { { "messege", "bad login or password"} }) :
                    Results.Json(new Date() { token = (Guid.NewGuid()), role = "student" });*/
                }
                catch (Exception ex)
                {
                    app.Logger.LogError($"User: {date.login}\nError: {ex.Message}");
                    return Results.Problem();
                }
            });

            ///<summary>
            ///Обнволение токена
            ///</summary>
            ///API ïåðåâûäà÷à òîêåíà
            app.Map("/newJWT", (HttpContext context) =>
            {
                app.Logger.LogInformation("ResponceJWT");
                var o = context.User.Identity;
                if (o is not null && o.IsAuthenticated)
                {
                    var claims = context.User.Claims;
                    foreach (var i in claims)
                    {
                        app.Logger.LogDebug("claim " + i.Value + i.ValueType + " " + i.Type + " " + i.Subject + " ");
                    }
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            claims: claims,
                            /// <summary>
                            /// Время жизни токена - 2 минуты
                            /// </summary>
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(40)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    app.Logger.LogInformation($"User: {context.User.Identity.Name} \nnewJWT: {jwt}");
                    var role = context.User.Claims.First(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Split('\n');
                    return Results.Json(new { jwt = new JwtSecurityTokenHandler().WriteToken(jwt), role = role});
                }
                app.Logger.LogError($"Error new JWT: {context.User.Identity.Name} " + o + " " + o.IsAuthenticated);
                return Results.NotFound();


            });

            ///<summary>
            ///Получение списка заявлений
            ///</summary>
            ///API ñïèñêà çàÿâëåíèé
            app.MapGet("/getstatmens/user:{Token}", [Authorize] async (string Token, HttpContext context, Gpo2Context cntx) =>
            {
                var UserMail = context.User.Identity.Name;
                var role = context.User.Claims.First(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Split('\n');
                
                var AskFormStudent = cntx.AskForms
                    .Include(x => x.StudentNavigation)
                    .Include(x=>x.StatusNavigation)
                    .Include(x=>x.ContractNavigation)
                    .ThenInclude(x=>x.PracticTimenNavigation);

                IQueryable<AskForm> Forms;
                if (role.Contains("Student"))
                {
                    Forms = AskFormStudent.Where(x => x.StudentNavigation.Email == UserMail);
                }
                else
                {
                    Forms = AskFormStudent.Where(x => x.Status == 2);
                }

                var Includers = Forms.Include(x => x.ContractNavigation);
                var Contracts = Includers.Select(x => x.ContractNavigation);

                ///<summary>
                ///Конструирование списка заявлений
                ///</summary>
                var Contaner1 = Forms.Where(x=>x.Status!=0).Select(x => new { Id = x.Id.ToString(), Type = "Заявление", Time = DateTime.Now, practicType = x.PracticeType, state = x.StatusNavigation.StatusName });
                var Contaner2 = Forms.Where(x => x.ContractNavigation.Status != 0).Select(x => new { Id = x.Id.ToString(), Type = "Договор", Time = DateTime.Now, practicType = x.PracticeType, state = x.ContractNavigation.StatusNavigation.StatusName });
                var result = Contaner1.Concat(Contaner2);
                return result;                
            });
            


            ///API çàÿâëåíèÿ
            ///<summary>
            ///Получение формы для запроса данных
            ///</summary>
            app.MapGet("/getformDate", [Authorize] async (string ID, string Type, HttpContext context, Gpo2Context cntx) => {

                Dictionary<string, string> Result = new Dictionary<string, string>();

#warning Добавить просмотр для рукводящей роли

                var role = context.User.Claims.First(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Split('\n');



                switch (Type)
                {
                    case "Договор":
                        Result.Add("id", ID);
                        Result.Add("Template", "Contract");
                        break;
                    case "Заявление":
                        Result.Add("id", ID);
                        Result.Add("Template", "AskForm");
                        break;
                    default:
                        return Results.NotFound();
                }

                var UserMail = context.User.Identity.Name;

                if (!role.Contains("Student"))
                {
                    var t1 = cntx.AskForms.FirstOrDefaultAsync(x => x.Id.ToString() == ID);
                    var t2 = cntx.Students.FindAsync((t1.GetAwaiter().GetResult() ?? throw new Exception ("No Find User!")).Student);
                    var t3 = (await cntx.Users.FindAsync((t2.GetAwaiter().GetResult()).User)).Email;
                    UserMail = t3;
                }

                var User = await cntx.Users
                    .Where(x => x.Email == UserMail)
                    .Include(x => x.Student)
                        .ThenInclude(x => x.GroupNavigation)
                        .ThenInclude(x => x.DirectionNavigation)
                        .ThenInclude(x => x.LeaderNavigation)
                    .Include(x => x.Student)
                        .ThenInclude(x => x.GroupNavigation)
                        .ThenInclude(x => x.CafedralNavigation)
                        .ThenInclude(x => x.LeaderNavigation)
                    .FirstAsync(x => x.Email == UserMail);



                ///<summary>
                ///Создать новое заявление
                ///</summary>
                if (ID == "New")
                {
                    switch (Type)
                    {
                        case "Заявление":

                            Contract contract = new Contract()
                            {
                                Status = 0,
                                
                                Organisation = 1,
                            };
                            contract.Number = contract.Id.ToString();

                            AskForm AskForm = new AskForm()
                            {
                                
                                PracticeLeaderNavigation = User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation,
                                PracticeLeader = User.Student.GroupNavigation.DirectionNavigation.LeaderName,
                                ConsultantLeaderNavigation = User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation,
                                ConsultantLeader = User.Student.GroupNavigation.DirectionNavigation.LeaderName,
#warning Выяснить кто есть руководитель консультант
                                AskFormResposebleNavigation = User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation,
                                AskFormResposeble = User.Student.GroupNavigation.DirectionNavigation.LeaderName,
#warning Выяснить кто отвественен за заполнение
                                ContractNavigation = contract,
                                Contract = contract.Id,
                                GroupNavigation = User.Student.GroupNavigation,
                                Group = User.Student.Group,
                                StudentNavigation = User,
                                Student = User.Id,
                                PracticeType = 1,
                                Status = 1,
                            };
                            
                            await cntx.AskForms.AddAsync(AskForm);
                            await cntx.SaveChangesAsync();
                            Result["id"] = await cntx.AskForms.Where(x => x.StudentNavigation.Email == UserMail).MaxAsync(x => x.Id).ContinueWith(x => x.Result.ToString());
                            ID = Result["id"];
                            break;
                        case "Договор":
                            Result["id"] = await cntx.AskForms.Where(x => x.StudentNavigation.Email == UserMail).MaxAsync(x => x.Id).ContinueWith(x => x.Result.ToString());
                            ID = Result["id"];
                            var form = await cntx.AskForms.FirstAsync(x => x.Id == Int32.Parse(ID));
                            contract = await cntx.Contracts.FirstAsync(x=>x.Id==form.Contract);
                            contract.Status = 1;
                            cntx.Contracts.Update(contract);
                            await cntx.SaveChangesAsync();
                            break;
                    }
                }


                int NumID = Int32.Parse(ID);
                var askForms = cntx.AskForms.Include(x => x.ContractNavigation);




                var askFormSeq = askForms
                   .Where(x => x.Id == NumID)
                   .Include(x => x.PracticeTypeNavigation)
                   .Include(x => x.ContractNavigation)
                   .ThenInclude(x => x.PracticTimenNavigation)
                   .Include(x => x.ContractNavigation.OrganizationNavigation);
                   //.ThenInclude(x => x.OrganizationNavigation);

                AskForm askForm;


                if (role.Contains("Student"))
                {
                    try
                    {
                        askForm = await askFormSeq.FirstAsync(x => x.StudentNavigation.Email == UserMail && x.Id == NumID);

                    }
                    catch(Exception ex)
                    {
                        return Results.StatusCode(401);
                    }
                }
                else
                    askForm = await askFormSeq.FirstAsync(x => x.Id == NumID);
                
                if (askForm.Commentary is not null)
                    Result.Add("Commentary", askForm.Commentary);

                switch (Type)
                {
                    case "Заявление":
                        Result.Add("State", askForm.Status.ToString());
                        Result.Add("Cafedral", User.Student.GroupNavigation.CafedralNavigation.EncriptedName ?? "");
                        Result.Add("Cafedral Leader", $"{User.Student.GroupNavigation.CafedralNavigation.LeaderNavigation.LastName ?? ""}" +" "+
                            $"{User.Student.GroupNavigation.CafedralNavigation.LeaderNavigation.FirstName ?? ""}" +" "+
                            $"{User.Student.GroupNavigation.CafedralNavigation.LeaderNavigation.MiddleName ?? ""}");
                        Result.Add("Group", User.Student.GroupNavigation.Groups ?? "");
                        Result.Add("StudentName", $"{User.LastName ?? ""} {User.FirstName ?? ""} {User.MiddleName ?? ""}");
                        Result.Add("Practic Type", askForm.PracticeTypeNavigation.Name ?? "");
                        Result.Add("Practic Sort", askForm.PracticeTypeNavigation.Name ?? "");
                        Result.Add("FactoryName", askForm.ContractNavigation.OrganizationNavigation.Name ?? "");
                        Result.Add("FactoryAdress", askForm.ContractNavigation.OrganizationNavigation.Adress ?? "");
                        Result.Add("StartDate", askForm.ContractNavigation.PracticTimenNavigation.DateStart.ToShortDateString().Replace('/', '.') ?? "");
                        Result.Add("EndDate", askForm.ContractNavigation.PracticTimenNavigation.DateEnd.ToShortDateString().Replace('/', '.') ?? "");
                        Result.Add("AskFormTime", DateTime.Now.ToShortDateString().Replace('/', '.') ?? "");
                        Result.Add("Cafedral Practic Leader", $"{User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.LastName ?? ""}" +
                            $" {User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.FirstName ?? ""}" +
                            $" {User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.MiddleName ?? ""}");
                        break;
                    case "Договор":
                        if (askForm.ContractNavigation.Status == 0)
                            return Results.NotFound();
                        Result.Add("State", askForm.ContractNavigation.Status.ToString());
                        Result.Add("ContractNumber", askForm.ContractNavigation.Number ?? "");
                        Result.Add("ContractDate", DateTime.Now.ToShortDateString().Replace('/', '.') ?? "");
                        Result.Add("FactoryName", askForm.ContractNavigation.OrganizationNavigation.Name ?? "");
                        Result.Add("OrganizationRule", askForm.ContractNavigation.OrganizationNavigation.Document ?? "");
                        Result.Add("DerictionType", User.Student.GroupNavigation.DirectionNavigation.Name ?? "");
                        Result.Add("FactoryLeaderName", askForm.ContractNavigation.OrganizationNavigation.FactoryLeader ?? "");
                        Result.Add("FactoryLocation", askForm.ContractNavigation.OrganizationNavigation.Adress ?? "");
                        Result.Add("FactoryRank", (askForm.ContractNavigation.OrganizationNavigation.Rank ?? ""));
                        Result.Add("CafedralPracticFielderLeader", "И.А. Трубчинова");
                        Result.Add("Practic Type", askForm.PracticeTypeNavigation.Name ?? "");
                        Result.Add("StudentOnFactoryCount", askForm.ContractNavigation.AskForms.Count().ToString() ?? "");
                        Result.Add("StudentName", $"{User.LastName ?? ""} {User.FirstName ?? ""} {User.MiddleName ?? ""}");
                        Result.Add("Curse", User.Student.GroupNavigation.Cours.ToString() ?? "");
                        Result.Add("Group", User.Student.GroupNavigation.Groups ?? "");
                        Result.Add("TimePrepand", $"{(askForm.ContractNavigation.PracticTimenNavigation.DateEnd.DayNumber - (askForm.ContractNavigation.PracticTimenNavigation.DateStart.DayNumber))} дней");
#warning Исправить
                        Result.Add("Cafedral Practic Leader", $"{User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.LastName ?? ""}" + " "+
                            $" {User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.FirstName ?? ""}" + " "+
                            $" {User.Student.GroupNavigation.DirectionNavigation.LeaderNavigation.MiddleName ?? ""}");
                        var t1 = cntx.AskForms
                            .FirstAsync(x => x.Id == NumID)
                            .ContinueWith(x => x.Result.ContractNavigation.Room ?? "")
                            .ContinueWith(x=>new KeyValuePair<string, string> ("WorksRooms", x.Result));
                        var t2 = cntx.AskForms
                            .FirstAsync(x => x.Id == NumID)
                            .ContinueWith(x=>x.Result.ContractNavigation.OrganizationNavigation.Adress ?? "")
                            .ContinueWith(x => new KeyValuePair<string, string>("WorkRoomAddress", x.Result));
                        var t3 = cntx.AskForms
                            .FirstAsync(x => x.Id == NumID)
                            .ContinueWith(x => x.Result.ContractNavigation.Equipment ?? Array.Empty<string>())
                            .ContinueWith(x=>x.Result.Aggregate(new StringBuilder(), (x, y) => x
                                .Append('&')
                                .Append(y))
                            .ToString() ?? "")
                            .ContinueWith(x => new KeyValuePair<string, string>("Practic Used Tools", x.Result));

                        var results = await Task.WhenAll(t1, t2, t3);
                        foreach (var pair in results)
                        {
                            Result.Add(pair.Key, pair.Value);
                        }
                        break;
                    default:
                        return Results.NotFound();
                }

                return Results.Json(Result);

            });
            //app.MapGet("/getformDate:{TypePost}", [Authorize] (string TypePost) => new { id = TypePost + "new", Template = TypePost });

            ///API Ïîëó÷åíèå ïîëåé äàííûõ
            ///<summary>
            ///Запись значений
            ///</summary>
            app.MapPost("/getInfo", [Authorize] async (int? ID, string Template, Dictionary<string, string> UserForm, HttpContext context, Gpo2Context cntx) =>
            {
                try { 
                var role = context.User.Claims.First(x=>x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Split('\n');
                var UserMail = context.User.Identity.Name;
                var User = await cntx.Users.FirstOrDefaultAsync(x => x.Email == UserMail);
                if (User is null)
                {
                    return Results.BadRequest("User Not Found");
                }

                AskForm askForm;

                if (role.Contains("PracticFieldLeader"))
                { 
                    askForm = (await cntx.AskForms.FindAsync(ID));
                    askForm.Commentary = UserForm["Commentary"] ?? "";
                    switch (Template)
                        {
                            case "AskForm":
                                askForm.Status = UserForm["Confirm"] == "Confirm" ? askForm.Status + 1 : askForm.Status - 1;
                                break;
                            case "Contract":
                                var contract = await cntx.Contracts.FindAsync(askForm.Contract);
                                contract.Status = UserForm["Confirm"] == "Confirm" ? contract.Status + 1 : contract.Status - 1;
                                break;
                        }
                    cntx.AskForms.Update(askForm);
                    await cntx.SaveChangesAsync();
                    return Results.Ok("success");
                }

                askForm = await cntx.AskForms
                    .Include(x => x.StudentNavigation)
                    .FirstAsync(x => x.Id == ID.Value && x.StudentNavigation.Email == UserMail);


                    Organization? organisation;
                    switch (Template)
                    {
                        case "Contract":
                            {
                                var DoubledContract = await cntx.Contracts
                                    .FirstOrDefaultAsync(x => x.OrganizationNavigation.Name == UserForm["FactoryName"]
                                        && x.OrganizationNavigation.Adress == UserForm["FactoryLocation"]);
#warning Добавить Руководителя практики от организации ^
                                switch (DoubledContract, askForm)
                                {
                                    ///<!--Договор с этим предприятием есть, студент остался в нём-->
                                    case (Contract, AskForm) ValuePair when ValuePair.askForm.Contract == ValuePair.DoubledContract.Id:
                                        ValuePair.DoubledContract.Status = ValuePair.DoubledContract.Status++;
                                        break;
                                    ///<!--Договор с этим предприятием есть, студент изменил предприятие-->
                                    case (Contract, AskForm) ValuePair:
                                        var oldcontract = ValuePair.askForm.Contract;
                                        ValuePair.askForm.Contract = ValuePair.DoubledContract.Id;
                                        if (cntx.AskForms.Where(x => x.Contract == oldcontract).Count() == 0)
                                        {
                                            var oldContractEntity = await cntx.Contracts.FirstAsync(x => x.Id == oldcontract);
                                            organisation = await cntx.Organizations.FindAsync(oldContractEntity.Organisation);
                                            if (organisation != null)
                                                cntx.Organizations.Remove(organisation);
                                            cntx.Contracts.Remove(oldContractEntity);
                                        }
                                        ValuePair.DoubledContract.Status = ValuePair.DoubledContract.Status++;
                                        await cntx.SaveChangesAsync();
                                        break;
                                    ///<!--Такого договора не существует-->
                                    case (null, _):
                                        Organization neworganisation = new Organization()
                                        {
                                            Name = UserForm["FactoryName"] ?? "",
                                            Adress = UserForm["FactoryLocation"] ?? "",
                                            Rank = UserForm["FactoryRank"] ?? "",
                                            FactoryLeader = UserForm["FactoryLeaderName"] ?? "",
                                            Document = UserForm["OrganizationRule"] ?? "",
                                        };
                                        cntx.Organizations.Add(neworganisation);
                                        var icnludeduser = await cntx.Users
                                            .Where(x => x == User)
                                            .Include(x => x.Student)
                                            .ThenInclude(x => x.GroupNavigation)
                                            .ThenInclude(x => x.DirectionNavigation)
                                            .ThenInclude(x => x.PracticTymes)
                                            .FirstAsync();
                                        Contract newcontract = new Contract()
                                        {
                                            OrganizationNavigation = neworganisation,
                                            Number = cntx.Contracts.Select(x => x.Number).Max() + 1,
                                            Room = UserForm["WorksRooms"],
                                            Status = (await cntx.Contracts.FindAsync(askForm.Contract)).Status,
                                            Equipment = UserForm["Practic Used Tools"].Split('&'),
                                            PracticTimenNavigation = icnludeduser.Student!.GroupNavigation.DirectionNavigation.PracticTymes.First(x => x.Year == DateTime.Now.Year)
                                        };
                                        cntx.Contracts.Add(newcontract);
                                        askForm.ContractNavigation = newcontract;
                                        break;
                                }
                            }

                            break;
                        case "AskForm":
                            askForm.Status++;
                            var contract = await cntx.Contracts.FindAsync(askForm.Contract);
                            organisation = await cntx.Organizations.Where(x => x.Name == UserForm["FactoryName"] && x.Adress == UserForm["FactoryAdress"]).FirstOrDefaultAsync();
                            if (organisation is not null)
                            {
                                contract.OrganizationNavigation = organisation;
                            }
                            else
                            {
                                Organization neworganisation = new Organization()
                                {
                                    Name = UserForm["FactoryName"] ?? "",
                                    Adress = UserForm["FactoryLocation"] ?? "",
                                    Rank = UserForm["FactoryRank"] ?? "",
                                    FactoryLeader = UserForm["FactoryLeaderName"] ?? "",
                                    Document = UserForm["OrganizationRule"] ?? "",
                                };

                                contract.OrganizationNavigation = neworganisation;
                            }
                            
                            await cntx.SaveChangesAsync();
                            break;
                    }
                    return Results.Ok("success");

                }
                catch (Exception ex)
                {
                    return Results.InternalServerError();
                }



                Console.WriteLine("------------------------------------------------");
                string accamulator = "";

                if (temp.TryAdd(Int32.Parse(UserForm["id"]), new Dictionary<string, string>()))
                {
                    app.Logger.LogError($"AddedStatmen: {UserForm["id"]}");
                }

                int id = Int32.Parse(UserForm["id"]);

                UserForm.Remove("id");

                ///Çàïîëíåíèå àêêàìóëÿòîðà äëÿ ëîãà + äîáàâëåíèå â ñëîâàðü
                foreach (var item in UserForm)
                {
                    accamulator += $"{item.Key}: {(item.Value == null || item.Value == ("") ? ("none") : item.Value)}: {AddOnDictionary(temp[id], item)}\n";
                    //Console.WriteLine($"{item.Key} ^ {item.Value} - WriteLine");
                }
                app.Logger.LogInformation((new EventId(calculator++, "getInfo")), accamulator);
                return Results.Ok("sucsefull");
            });

            /// <summary>
            /// API получение шаблона
            /// Ошибка - не тот шаблон
            /// </summary>

            app.MapGet("/getTepmlate/{TemplateName}", [Authorize] async (string TemplateName, HttpContext context) =>
            {

                return RequestTemplates[TemplateName];
            });

            //API øàáëîíà ïå÷àòè
            app.MapGet("/GetPrintAtribute/{TemplateName}", [Authorize] (string TemplateName) => 
            { 
                switch (TemplateName)
                {
                    case "Заявление":
                        TemplateName = "AskForm";
                        break;
                    case "Договор":
                        TemplateName = "Contract";
                        break;

                }
                return cntx.Templates.Where(x => x.Name == TemplateName).FirstOrDefault().TemplateBody;
                
            });
            
            app.UseStaticFiles();
            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run(); 
        }
    }

    public class IdentetyAuthenticationStateProvider: AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState( new System.Security.Claims.ClaimsPrincipal() );
        }
    }
}
