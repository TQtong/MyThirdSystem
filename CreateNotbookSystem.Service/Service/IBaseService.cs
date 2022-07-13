namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 服务操作方法基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync();

        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(T model);

        Task<ApiResponse> UpdateAsync(T model);

        Task<ApiResponse> DeleteAsync(int id);
    }
}
