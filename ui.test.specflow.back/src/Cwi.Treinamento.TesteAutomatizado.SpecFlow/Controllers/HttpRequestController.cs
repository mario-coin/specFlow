using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers
{
    public class HttpRequestController
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly Uri baseUrl;
        private HttpRequestMessage httpRequestMessage;
        private HttpResponseMessage httpResponseMessage;

        public HttpRequestController(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            this.httpClientFactory = httpClientFactory;
            this.baseUrl = new Uri(baseUrl);
        }

        private HttpRequestMessage GetRequestMessage()
        {
            if (httpRequestMessage == null)
            {
                httpRequestMessage = new HttpRequestMessage();
            }

            return httpRequestMessage;
        }

        public void RemoveHeader(string name)
        {
            GetRequestMessage().Headers.Remove(name);
        }

        public void AddHeader(string name, string value)
        {
            GetRequestMessage().Headers.Add(name, value);
        }

        public void AddJsonBody(object body)
        {
            GetRequestMessage().Content = PrepareJsonBody(body);
        }

        private HttpContent PrepareJsonBody(object body)
        {
            if (body.GetType().IsPrimitive || body is string)
            {
                return new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            }

            return new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
        }

        public async Task SendAsync(string endpoint, string httpMethodName)
        {
            var request = GetRequestMessage();

            request.RequestUri = new Uri(baseUrl, endpoint);
            request.Method = GetHttpFromName(httpMethodName);

            httpResponseMessage = await httpClientFactory.CreateClient().SendAsync(request);

            httpRequestMessage.Dispose();
            httpRequestMessage = null;
        }

        public HttpStatusCode GetResponseHttpStatusCode()
        {
            return httpResponseMessage?.StatusCode ?? HttpStatusCode.NotFound;
        }

        public async Task<T> GetTypedResponseBody<T>()
        {
            var responseContext = await GetResponseBodyContent();

            return JsonConvert.DeserializeObject<T>(responseContext);
        }

        private async Task<string> GetResponseBodyContent()
        {
            using (var httpContent = httpResponseMessage.Content)
            {
                return await httpContent.ReadAsStringAsync(); 
            }
        }

        private HttpMethod GetHttpFromName(string httpMethodName)
        {
            switch(httpMethodName.ToLower())
            {
                case "get":
                    return HttpMethod.Get;
                case "post":
                    return HttpMethod.Post;
                case "patch":
                    return HttpMethod.Patch;
                case "put":
                    return HttpMethod.Put;
                case "delete":
                    return HttpMethod.Delete;
                case "options":
                    return HttpMethod.Options;
                case "head":
                    return HttpMethod.Head;
                default:
                    return HttpMethod.Get;
            }
        }
    }
}
