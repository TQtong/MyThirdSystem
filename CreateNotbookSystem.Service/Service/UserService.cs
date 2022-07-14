using AutoMapper;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 用户的实现
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(UserDto model)
        {
            try
            {
                var user = mapper.Map<User>(model);

                var repository = work.GetRepository<User>();
                await repository.InsertAsync(user);

                var result = await work.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiResponse(true, user);
                }
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                if (user == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

                repository.Delete(user);

                var result = await work.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiResponse(true, "删除数据成功");
                }
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(UserDto model)
        {
            try
            {
                var user = mapper.Map<User>(model);

                var repository = work.GetRepository<User>();

                var userDb = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(user.Id));

                if (userDb == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

                userDb.Account = user.Account;
                userDb.Name = user.Name;
                userDb.Password = user.Password;
                userDb.UpdatedDate = DateTime.Now;

                repository.Update(userDb);

                var result = await work.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiResponse(true, userDb);
                }
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, user);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var users = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Equals(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreatedDate));

                return new ApiResponse(true, users);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
