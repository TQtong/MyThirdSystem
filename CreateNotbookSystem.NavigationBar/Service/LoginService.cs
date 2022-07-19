using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;

        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse> LoginAsync(UserDto param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/Login/Login";
            request.Parameter = param;

            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/Login/Register";
            request.Parameter = param;

            return await client.ExecuteAsync(request);
        }
    }
}
