using CreateNotbookSystem.Common;
using CreateNotbookSystem.Common.Configurations;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Service
{
    public class MemoService : BaseService<MemoDto>, IMemoService
    {

        public MemoService(HttpRestClient client) : base(client, "Memo")
        {
        }
    }
}
