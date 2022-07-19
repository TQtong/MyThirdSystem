using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto param) =>
            await service.LoginAsync(param.Account, param.Password);

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto param) =>
            await service.RegisterAsync(param);

    }
}
