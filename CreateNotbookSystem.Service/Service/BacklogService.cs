using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 待办事项的实现
    /// </summary>
    public class BacklogService : IBacklogService
    {
        private readonly IUnitOfWork work;

        public BacklogService(IUnitOfWork work)
        {
            this.work = work;
        }

        public async Task<ApiResponse> AddAsync(Backlog model)
        {
            try
            {
                var repository = work.GetRepository<Backlog>();
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
                var repository = work.GetRepository<Backlog>();
                var backlog = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                repository.Delete(backlog);

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
                var repository = work.GetRepository<Backlog>();
                var backlogs = await repository.GetAllAsync();

                return new ApiResponse(true, backlogs);

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
                var repository = work.GetRepository<Backlog>();
                var backlog = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, backlog);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(Backlog model)
        {
            try
            {
                var repository = work.GetRepository<Backlog>();

                var backlog = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                backlog.Status = model.Status;
                backlog.Content = model.Content;
                backlog.Title = model.Title;
                backlog.UpdatedDate = DateTime.Now;

                repository.Update(backlog);

                var result = await work.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiResponse(true, backlog);
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
