using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class TallyConnectedResponseModel
    {
        public bool Status { get; set; }
        public string Details { get; set; }

        public TallyConnectedResponseModel(bool status = false, string details = "")
        {
            this.Status = status;
            this.Details = details;
        }
    }
}
