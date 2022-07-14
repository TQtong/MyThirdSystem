using CreateNotbookSystem.Common.DbContent.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public class BacklogService : BaseService<BacklogDto>, IBacklogService
    {
        public BacklogService(HttpRestClient client) : base(client, "Backlog")
        {
        }
    }
}
