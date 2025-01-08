using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace GPO_BLAZOR.Client.Class.Field
{
    public partial class UGRNU : Field
    {
        private string SearchField { get; set; }
        private record struct FactoryValue
        {
            [JsonPropertyName("c")]
            public string Name { get; init; }
            [JsonPropertyName("g")]
            public string LeaderName { get; init; }
            [JsonPropertyName("t")]
            public string Token { get; init; }
            [JsonPropertyName("rn")]
            public string Region { get; init; }
        }

        private record struct SearchAttribute
        {
            [JsonPropertyName("t")]
            string Token { get; set; }
            [JsonPropertyName("captchaRequired")]
            string CaptchaRequired { get; set; }
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
            try
            {

                HttpRequestMessage PosthttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://egrul.nalog.ru");
                PosthttpRequestMessage.Content = new StringContent($"query={SearchField}");
                PosthttpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                PosthttpRequestMessage.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("UTF8"));
                PosthttpRequestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");
                PosthttpRequestMessage.Headers.Add("Sec-Fetch-Mode", "no-cors");
                PosthttpRequestMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                httpClient.DefaultRequestHeaders.Add("mode", "no-cors");
                httpClient.DefaultRequestHeaders.Remove("Sec-Fetch-Mode");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "no-cors");

                var res =  httpClient.SendAsync(PosthttpRequestMessage).ContinueWith(responce =>
                {
                    if (responce.Result.IsSuccessStatusCode)
                    {
                        return responce.Result.Content.ReadFromJsonAsync<SearchAttribute>().ContinueWith(x =>
                        {
                            HttpRequestMessage GethttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://egrul.nalog.ru/search-result/{x.Result}");
                            GethttpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                            GethttpRequestMessage.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("UTF8"));
                            GethttpRequestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");
                            GethttpRequestMessage.Headers.Add("Sec-Fetch-Mode", "no-cors");
                            GethttpRequestMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                            return httpClient.SendAsync(GethttpRequestMessage).ContinueWith(static getresponce =>
                                getresponce.Result.Content.ReadFromJsonAsAsyncEnumerable<FactoryValue>());
                        })
                            .Unwrap();
                    }
#warning NullableValue!!!!
                    throw new Exception($"Status code<{responce.Result.StatusCode}> ");
                })
                    .Unwrap();
                httpClient.DefaultRequestHeaders.Remove("Sec-Fetch-Mode");
                httpClient.DefaultRequestHeaders.Remove("mode");

                return res;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Can't send UGRNUL -> "+ex.ToString());
                return null;
            }
        }
#nullable restore


        private FactoryValue _actualValue { get; set; }
        private IEnumerable<FactoryValue> _actualList { get; set; } = Enumerable.Empty<FactoryValue>();

        [Inject]
        private HttpClient httpClient {get; set;}
    }
}
