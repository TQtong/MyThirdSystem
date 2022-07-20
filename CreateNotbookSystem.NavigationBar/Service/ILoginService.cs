using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto param);
        Task<ApiResponse> RegisterAsync(UserDto param);
    }
}
