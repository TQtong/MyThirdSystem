using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
    /// <summary>
    /// 备忘录控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : Controller
    {
        private readonly IMemoService service;

        public MemoController(IMemoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetSingle(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto memo) => await service.AddAsync(memo);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto memo) => await service.UpdateAsync(memo);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);
    }
}
