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
        private CollectionValues(string[] value, string? ID = null)
        {

            this.ID = ID;
            if (value != null)
                Values = value;
            else Values = null;
        }

        public string[] Values { get; init; }

        public static async Task<CollectionValues> Create(string Name, IJSRuntime jsr, string? ID = null)
        {
            return new CollectionValues(await GetAtributes(Name, jsr, ID));
        }

        private static async Task<string[]> GetAtributes(string Field, IJSRuntime jsr, string? ID = null)
        {
            return await Requesting.AutorizationedGetRequest<string[]>(new Uri($"https://{IPaddress.IPAddress}/GetAtributes/{Field}{(ID is null?"":$"?ID={ID}")}"), jsr);
        }

    }
}