﻿using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CreateNotbookSystem.Service.Controllers
{
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
        public async Task<ApiResponse> GetGetSing(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll() => await service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> Add(User user) => await service.AddAsync(user);

        [HttpPost]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Update(User user) => await service.UpdateAsync(user);
    }
}
