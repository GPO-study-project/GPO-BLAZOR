using GPO_BLAZOR.Client.Class.Date;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MigraDoc.Rendering;

namespace GPO_BLAZOR.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped<AuthenticationStateProvider, IdentetyAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<IAutorizationStruct, AutorizationStruct>();
            builder.Services.AddScoped<CookieStorageAccessor>();
            builder.Services.AddScoped<LocalStorageAccessor>();
            builder.Services.AddScoped<PdfDocumentRenderer>();

            builder.Services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress.Trim('/')) });

            var app = builder.Build();


            var c = builder.HostEnvironment.BaseAddress;

            IPaddress.helper = c;
            var uri = new Uri(c);
            IPaddress.IPAddress = uri.Host + (uri.Port == null || uri.Port == 0 ? "": ":"+uri.Port);

            //Console.WriteLine("BaseAddress "+ c);

            //app.UseCors(builder => builder.WithOrigins("https://surnameonline.ru/").WithMethods("POST").AllowAnyHeader());

            await app.RunAsync();
        }
    }

    public class IdentetyAuthenticationStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());
        }
    }
}
