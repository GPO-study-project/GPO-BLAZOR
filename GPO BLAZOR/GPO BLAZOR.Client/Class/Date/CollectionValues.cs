using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace GPO_BLAZOR.Client.Class.Date
{

    /// <summary>
    /// Модель значений выпадющего списка
    /// </summary>
    public record CollectionValues
    {
        
        string? ID { get; set; }
        private CollectionValues(string[] value, HttpClient httpClient, string? ID = null)
        {

            this.ID = ID;
            this.httpClient = httpClient;
            if (value != null)
                Values = value;
            else Values = null;
        }

        public string[] Values { get; init; }
        private HttpClient httpClient { get; set; }
        public static Task<CollectionValues> Create(string Name, IJSRuntime jsr, HttpClient httpClient, string? ID = null)
        {
            var AttributesTask = GetAtributes(Name, jsr, httpClient, ID);
            return AttributesTask.ContinueWith(x=> new CollectionValues(x.Result , httpClient));
        }

        private static Task<string[]> GetAtributes(string Field, IJSRuntime jsr, HttpClient httpClient, string? ID = null)
        {
            return Requesting.AutorizationedGetRequest<string[]>($"GetAtributes/{Field}", httpClient, jsr);
        }

    }
}