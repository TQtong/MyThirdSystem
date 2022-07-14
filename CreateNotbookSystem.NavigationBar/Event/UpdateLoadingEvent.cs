using CreateNotbookSystem.Common.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Event
{
    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {
    }
}
