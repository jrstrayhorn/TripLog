using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TripLog.Services
{
    public class BaseHttpService
    {
        public BaseHttpService()
        {
        }

        protected async Task<T> SendRequestAsync<T>(
            Uri url,
            HttpMethod httpMethod = null,
            IDictionary<string, string> headers = null,
            object requestData = null
        )
        {
            var result = default(T);

            // default to GET
            var method = httpMethod ?? HttpMethod.Get;

            // serialize request data
            var data = requestData == null
                ? null
                : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url)) {

                // add request data to request
                if (data != null)
                    request.Content = new StringContent(
                        data,
                        Encoding.UTF8,
                        "application/json"
                    );

                // add headers to request
                if (headers != null)
                    foreach (var h in headers)
                        request.Headers.Add(h.Key, h.Value);

                // get response
                using (var handler = new HttpClientHandler()) {
                    using (var client = new HttpClient(handler)) {
                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead)) {
                            var content = response.Content == null
                                                  ? null
                                                  : await response
                                                  .Content
                                                  .ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                                result = JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                }
            }

            return result;
        }
    }
}
