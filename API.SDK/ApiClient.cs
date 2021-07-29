using API.SDK.Resources;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK
{
    public class ApiClient
    {
        private readonly ServiceHttpClient httpClient;
        public IIdentityResource Identities { get; set; }
        public IAccountResource Account { get; set; }


        public ApiClient(ServiceHttpClient httpClient)
        {
            Identities = RestService.For<IIdentityResource>(httpClient);
            Account = RestService.For<IAccountResource>(httpClient);

            this.httpClient = httpClient;
        }
    }
}
