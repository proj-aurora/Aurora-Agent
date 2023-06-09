using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACL
{
    internal class ACLClientHandler : DelegatingHandler
    {
        private readonly string _version;

        public ACLClientHandler()
        {
            InnerHandler = new HttpClientHandler();
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("ACL", _version));
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
