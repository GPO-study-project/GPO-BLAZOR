using GPO_BLAZOR.Components;
using Microsoft.AspNetCore.Components.Authorization;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using System.Xml;
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


namespace GPO_BLAZOR
{


    class Date
    {
        public Guid token { get; set; }
        public string jwt { get; set; }
        public string role { get; set; }
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

        public static void Main(string[] args)
        {
            
                /*{
                    FileStream str = new FileStream("person.json", FileMode.OpenOrCreate);
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        AllowTrailingCommas = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                    };
                    var hjobj = PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make();
                    var JSONSer = JsonSerializer.Serialize(hjobj, options);
                    byte[] inputBuffer = Encoding.Default.GetBytes(JSONSer);
                
                    str.Write(inputBuffer, 0, inputBuffer.Length);
                    str.Close();


                    XmlSerializer xmlSerializer = new(typeof(Document));
                    str = new FileStream("person.xml", FileMode.Create);

                // ïîëó÷àåì ïîòîê, êóäà áóäåì çàïèñûâàòü ñåðèàëèçîâàííûé îáúåêò
                    var rest = PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make();

                    xmlSerializer.Serialize(str, rest);


                    var rtfRender = new RtfDocumentRenderer();
                    rtfRender.Render(PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().Render(), "Contract.rtf", "./");

                    


                    Console.WriteLine("\nObject has been serialized\n");
                    var temp2 = PdfFilePrinting.MakeTemplate.MakeContractTemplate.Make().Render();

                    str.Close();
                    str = new FileStream("person.xml", FileMode.Open);
                    Document? res = xmlSerializer.Deserialize(str) as Document?;
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

            var urlstr = Environment.GetEnvironmentVariable("VS_TUNNEL_URL");
            var cntyui = Environment.GetEnvironmentVariables();
            Console.WriteLine($"Envirment Tunnel URL {urlstr}");
            Console.Write("Data Base Password: ");
            Gpo2Context cntx = new Gpo2Context(Console.ReadLine());


            // DBConnector.F(null);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<CookieStorageAccessor>();
            builder.Services.AddScoped<LocalStorageAccessor>();

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



            //builder.Services.AddAuthorizationCore();

            var app = builder.Build();

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


            ///API ñïèñêà ïîëåé
            app.MapGet("/GetAtributes/{Field}", (string Field) =>
            {
                try
                {
                    return SpecialArray[Field];
                }
                catch
                {
                    return new List<string>(){ "a", "b" };
                }
            });


            app.MapGet("/GetAtributes", () => new string[] { "A", "Á", "Â" });

            app.Logger.LogDebug("DEBUGSTART:");

            ///API àâòîðèçàöèè
            app.MapPost("/autorization", (Autorization.AutorizationDate date) =>
            {
                try
                {

                    if (!(Autorization.checkuser(date, cntx).Result))
                    {
                        return Results.Problem("not login or password", "nonautorization", 401, "bad login or password)", "nontype", new Dictionary<string, object> { { "messege", "bad login or password" } });
                    }

                    app.Logger.LogInformation($"User loging: {date.login}");

                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, date.login), new Claim(ClaimTypes.Role, "student") };
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // âðåìÿ äåéñòâèÿ 2 ìèíóòû
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));



                    return Results.Json(new Date(){
                        token = (Guid.NewGuid()),
                        jwt = new JwtSecurityTokenHandler().WriteToken(jwt), 
                        role = "student" 
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

            ///API ïåðåâûäà÷à òîêåíà
            app.Map("/newJWT", (HttpContext a) =>
            {
                app.Logger.LogInformation("ResponceJWT");
                var o = a.User.Identity;
                if (o is not null && o.IsAuthenticated)
                {
                    var claims = a.User.Claims;
                    foreach (var i in claims)
                    {
                        app.Logger.LogDebug("claim " + i.Value +i.ValueType+" "+i.Type+" "+i.Subject+" ");
                    }
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // âðåìÿ äåéñòâèÿ 2 ìèíóòû
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    app.Logger.LogInformation($"User: {a.User.Identity.Name} \nnewJWT: {jwt}");
                    return Results.Json(new { jwt = new JwtSecurityTokenHandler().WriteToken(jwt) });
                }
                app.Logger.LogError($"Error new JWT: {a.User.Identity.Name} "+o+" "+o.IsAuthenticated );
                return Results.NotFound();

                
            });

            ///API ñïèñêà çàÿâëåíèé
            app.MapGet("/getstatmens/user:{Token}",[Authorize]()=>b);

            ///API çàÿâëåíèÿ
            app.MapGet("/getformDate:{ID}", [Authorize] (string ID) => {
                int id;
                if (Int32.TryParse(ID, out id))
                {
                    app.Logger.LogInformation($"{ID}: {temp[id]}"); 
                    return Results.Json(temp[id]);
                }
                else
                {
                    return Results.Json(new { id = ID + "new", Template = ID });
                }
                
            });
            //app.MapGet("/getformDate:{TypePost}", [Authorize] (string TypePost) => new { id = TypePost + "new", Template = TypePost });

            ///API Ïîëó÷åíèå ïîëåé äàííûõ
            app.MapPost("/getInfo", (Dictionary<string, string> x)=>
            {
                Console.WriteLine("------------------------------------------------");
                string accamulator = "";

                if (temp.TryAdd(Int32.Parse(x["id"]), new Dictionary<string, string>()))
                {
                    app.Logger.LogError($"AddedStatmen: {x["id"]}");
                }

                int id = Int32.Parse(x["id"]);

                x.Remove("id");

                ///Çàïîëíåíèå àêêàìóëÿòîðà äëÿ ëîãà + äîáàâëåíèå â ñëîâàðü
                foreach (var item in x)
                {
                     accamulator+=$"{item.Key}: {(item.Value==null||item.Value==("")?("none"):item.Value)}: {AddOnDictionary(temp[id], item)}\n";
                    //Console.WriteLine($"{item.Key} ^ {item.Value} - WriteLine");
                }
                    app.Logger.LogInformation((new EventId(calculator++, "getInfo")), accamulator);
                    return Results.Ok("sucsefull");
            });
            FileStream fstream = new FileStream("person.xml", FileMode.Open);
            // âûäåëÿåì ìàññèâ äëÿ ñ÷èòûâàíèÿ äàííûõ èç ôàéëà
            byte[] buffer = new byte[fstream.Length];
            // ñ÷èòûâàåì äàííûå
            fstream.Read(buffer, 0, buffer.Length);
            // äåêîäèðóåì áàéòû â ñòðîêó
            string textFromFile = Encoding.Default.GetString(buffer);

            ///API øàáëîíà äîêóìåíòà
            app.MapGet("/GetTemplate", [Authorize]() => textFromFile);

            //API øàáëîíà ïå÷àòè
            app.MapGet("/GetPrintAtribute/{TemplateName}", [Authorize](string TemplateName) => PrintTemplate[TemplateName]);
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
