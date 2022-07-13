using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BacklogController : Controller
    {
        private readonly IMemoService service;

        public BacklogController(IMemoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetGetSing(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll() => await service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> Add(Memo memo) => await service.AddAsync(memo);

        [HttpPost]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Update(Memo memo) => await service.UpdateAsync(memo);
    }
}
