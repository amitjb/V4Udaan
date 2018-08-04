using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class TallyConnectedResquestModel
    {
        public string UserName { get; set; }
        public enumTallyConnectedStatus Status { get; set; }

        public TallyConnectedResquestModel(string UserName = "", enumTallyConnectedStatus Status = enumTallyConnectedStatus.nc)
        {
            this.UserName = UserName;
            this.Status = Status;
        }
    }
}