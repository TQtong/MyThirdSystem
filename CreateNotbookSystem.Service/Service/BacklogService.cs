using AutoMapper;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
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
        private readonly IMapper mapper;

        public BacklogService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(BacklogDto model)
        {
            try
            {
                var backlog = mapper.Map<Backlog>(model);

                var repository = work.GetRepository<Backlog>();
                await repository.InsertAsync(backlog);

                var result = await work.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiResponse(true, backlog);
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

                if (backlog == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

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

        public async Task<ApiResponse> UpdateAsync(BacklogDto model)
        {
            try
            {
                var backlog = mapper.Map<Backlog>(model);

                var repository = work.GetRepository<Backlog>();

                var backlogDb = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(backlog.Id));

                if (backlogDb == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

                backlogDb.Status = backlog.Status;
                backlogDb.Content = backlog.Content;
                backlogDb.Title = backlog.Title;
                backlogDb.UpdatedDate = DateTime.Now;

                repository.Update(backlogDb);

                var result = await work.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiResponse(true, backlogDb);
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
                var repository = work.GetRepository<Backlog>();
                var backlog = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, backlog);

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
                var repository = work.GetRepository<Backlog>();
                var backlogs = await repository.GetPagedListAsync(predicate: 
                    x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Equals(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreatedDate));

                return new ApiResponse(true, backlogs);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }




    }
}
