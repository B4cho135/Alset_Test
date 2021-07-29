using Microsoft.AspNetCore.Http;
using Services.Abstract.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK
{
    public class ServiceHttpClient : HttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceHttpClient(IHttpClientSettings settings, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            this.BaseAddress = new Uri(settings.ServiceUrl);
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Request.Cookies["access_token"]);
        }
    }
}
