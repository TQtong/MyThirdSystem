using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetSingle(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] UserDto user) => await service.AddAsync(user);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] UserDto user) => await service.UpdateAsync(user);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);
    }
}
