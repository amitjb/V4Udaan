using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class TallyConnectedCompaniesResquestModel
    {
        public string UserName { get; set; }
        public string CompanyNames { get; set; }

        public TallyConnectedCompaniesResquestModel(string userName = "",string companyNames = "")
        {
            this.UserName = userName;
            this.CompanyNames = companyNames;
        }
    }
}