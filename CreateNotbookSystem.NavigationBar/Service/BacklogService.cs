using CreateNotbookSystem.Common;
using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.Common.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public class BacklogService : BaseService<BacklogDto>, IBacklogService
    {
        private readonly HttpRestClient client;

        public BacklogService(HttpRestClient client) : base(client, "Backlog")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<BacklogDto>>> GetAllFilterAsync(BacklogQueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/Backlog/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}" +
                $"&status={parameter.Status}";

            return await client.ExecuteAsync<PagedList<BacklogDto>>(request);
        }

        public async Task<ApiResponse<SummaryModel>> GetSummaryAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/Backlog/Summary";

            return await client.ExecuteAsync<SummaryModel>(request);
        }
    }
}
