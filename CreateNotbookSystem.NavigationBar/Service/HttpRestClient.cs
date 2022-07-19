using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    /// <summary>
    /// 通过请求类
    /// </summary>
    public class HttpRestClient
    {
        private readonly string apiUrl;

        protected RestClient client;//客户端

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            client = new RestClient(apiUrl);
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            if (baseRequest.Parameter != null)
            {
                request.AddJsonBody(baseRequest.Parameter);
            }

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            client = new RestClient(apiUrl);
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            if (baseRequest.Parameter != null)
            {
                request.AddJsonBody(baseRequest.Parameter);
            }

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
