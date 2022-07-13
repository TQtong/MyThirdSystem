using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.UnitOfWork;

namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 备完录的实现
    /// </summary>
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork work;

        public MemoService(IUnitOfWork work)
        {
            this.work = work;
        }

        public async Task<ApiResponse> AddAsync(Memo model)
        {
            try
            {
                var repository = work.GetRepository<Memo>();
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
                var repository = work.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                if (memo == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

                repository.Delete(memo);

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
                var repository = work.GetRepository<Memo>();
                var memos = await repository.GetAllAsync();

                return new ApiResponse(true, memos);

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
                var repository = work.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, memo);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(Memo model)
        {
            try
            {
                var repository = work.GetRepository<Memo>();

                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                if (memo == null)
                {
                    return new ApiResponse("数据库中无该数据");
                }

                memo.Content = model.Content;
                memo.Title = model.Title;
                memo.UpdatedDate = DateTime.Now;

                repository.Update(memo);

                var result = await work.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiResponse(true, memo);
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
