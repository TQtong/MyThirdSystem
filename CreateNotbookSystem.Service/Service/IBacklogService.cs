using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
using CreateNotbookSystem.Service.Context;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 待办事项服务接口
    /// </summary>
    public interface IBacklogService : IBaseService<BacklogDto>
    {
        Task<ApiResponse> GetAllAsync(BacklogQueryParameter query);

        Task<ApiResponse> SummaryAsync();
    }
}
