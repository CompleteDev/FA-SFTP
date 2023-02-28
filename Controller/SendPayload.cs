using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using FollettSFTP.Options;
using FollettSFTP.Interfaces;
using Microsoft.Extensions.Options;

namespace FollettSFTP.Controller
{
    public class SendPayload : ISendPayload
    {
        private readonly System.Net.Http.IHttpClientFactory _clientFactory;
        private readonly MyServerOptions _myServerOptions;
        public SendPayload(System.Net.Http.IHttpClientFactory clientFactory, IOptions<MyServerOptions> myServerOptions)
        {
            _clientFactory = clientFactory;
            _myServerOptions = myServerOptions?.Value ?? throw new ArgumentNullException(nameof(myServerOptions));
        }

        public async Task SendASNPayload(string jsonData)
        {
            try
            {
                string APIURI = Environment.GetEnvironmentVariable("ASNURL");
                var client = _clientFactory.CreateClient();
                var requestContent = new StringContent(jsonData);
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(APIURI, requestContent);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }


            //Console.WriteLine(jsonData);
        }
    }
}
