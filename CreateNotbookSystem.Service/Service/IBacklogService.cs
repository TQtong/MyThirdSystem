using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Service.Context;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 待办事项服务接口
    /// </summary>
    public interface IBacklogService : IBaseService<BacklogDto>
    {
    }
}
