using CreateNotbookSystem.Common.DbContent.Dto;

namespace CreateNotbookSystem.Service.Service
{
    public interface ILoginService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<ApiResponse> LoginAsync(string Account, string Password);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse> RegisterAsync(UserDto user);
    }
}
