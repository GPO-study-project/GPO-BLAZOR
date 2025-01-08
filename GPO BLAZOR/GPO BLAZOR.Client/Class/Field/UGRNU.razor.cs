using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace GPO_BLAZOR.Client.Class.Field
{
    public partial class UGRNU : Field
    {
        private string SearchField { get; set; }
        record class FactoryValue
        {
            [JsonPropertyName("n")]
            public string Name { get; init; }
            public string LeaderName { get; init; }
        }

        IEnumerable<IEnumerable<FactoryValue>> GetPage(int size)
        {
            if (_actualList is not null)
            {
                return _actualList
                    .Select(static (value, num) => (value, num))
                    .GroupBy(x => x.num % size)
                    .Select(static x => x.
                        Select(static y => y.value));
            }
            return Enumerable.Empty<IEnumerable<FactoryValue>>();
        }

#nullable disable
        Task<IAsyncEnumerable<FactoryValue>> Search ()
        {
            HttpRequestMessage PosthttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://egrul.nalog.ru");
            PosthttpRequestMessage.Content = new StringContent($"query={SearchField}");
            PosthttpRequestMessage.Headers.Add("mode", "no-cors");
            return httpClient.SendAsync(PosthttpRequestMessage).ContinueWith(responce =>
            {
                if (responce.Result.IsSuccessStatusCode)
                {
                    return responce.Result.Content.ReadFromJsonAsync<SearchAttribute>().ContinueWith(x =>
                    {
                        HttpRequestMessage GethttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://egrul.nalog.ru/search-result/{x.Result}");
                        PosthttpRequestMessage.Headers.Add("mode", "no-cors");
                        return httpClient.SendAsync(GethttpRequestMessage).ContinueWith(static getresponce =>
                            getresponce.Result.Content.ReadFromJsonAsAsyncEnumerable<FactoryValue>());
                    })
                        .Unwrap();
                }
#warning NullableValue!!!!
                return null;
            })
                .Unwrap();
        }
#nullable restore

        private record struct SearchAttribute
        {
            string t { get; set; }
            string captchaRequired { get; set; }
        }


        FactoryValue _actualValue { get; set; }
        IEnumerable<FactoryValue> _actualList;

        [Inject]
        private HttpClient httpClient {get; set;}
    }
}
