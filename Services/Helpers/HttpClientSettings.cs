using Microsoft.Extensions.Configuration;
using Services.Abstract.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class HttpClientSettings : IHttpClientSettings
    {
        private readonly IConfiguration _configuration;
        public HttpClientSettings(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        public string ServiceUrl => _configuration["ApiBaseURL"];
    }
}
