using CreateNotbookSystem.Common;
using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.Common.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public interface IBacklogService : IBaseService<BacklogDto>
    {
        Task<ApiResponse<PagedList<BacklogDto>>> GetAllFilterAsync (BacklogQueryParameter parameter);

        Task<ApiResponse<SummaryModel>> GetSummaryAsync();
    }
}
