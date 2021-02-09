using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VictoryLinkTask.ServiceLayer.Dto.Request
{
    public class ReceiveRequestInputDto
    {
        public int MobileNumber { get; set; }
        public string Action { get; set; }
    }
}