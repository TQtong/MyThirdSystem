using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 待办事项的实现
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork work;

        public UserService(IUnitOfWork work)
        {
            this.work = work;
        }

        public async Task<ApiResponse> AddAsync(User model)
        {
            try
            {
                var repository = work.GetRepository<User>();
                await repository.InsertAsync(model);

                var result = await work.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiResponse(true, model);
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

        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = work.GetRepository<User>();
                var users = await repository.GetAllAsync();

                return new ApiResponse(true, users);

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

        public async Task<ApiResponse> UpdateAsync(User model)
        {
            try
            {
                var repository = work.GetRepository<User>();

                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                user.Account = model.Account;
                user.Name = model.Name;
                user.Password = model.Password;
                user.UpdatedDate = DateTime.Now;

                repository.Update(user);

                var result = await work.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiResponse(true, user);
                }
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }
    }
}
