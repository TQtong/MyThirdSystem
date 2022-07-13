using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : Controller
    {
        private readonly IBacklogService service;

        public MemoController(IBacklogService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetGetSing(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll() => await service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> Add(Backlog backlog) => await service.AddAsync(backlog);

        [HttpPost]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Update(Backlog backlog) => await service.UpdateAsync(backlog);
    }
}
