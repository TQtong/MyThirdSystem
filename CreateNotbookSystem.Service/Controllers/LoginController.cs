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

        [HttpGet]
        public async Task<ApiResponse> Login(string account, string password) =>
            await service.LoginAsync(account, password);

        [HttpPost]
        public async Task<ApiResponse> Resgiter([FromBody] UserDto param) =>
            await service.ResgiterAsync(param);

    }
}
