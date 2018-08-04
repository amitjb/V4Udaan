using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class TallyDayBookInfoResquestModel
    {
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public enumXMLType XML_Type { get; set; }
        public string XML { get; set; }


        public TallyDayBookInfoResquestModel(enumXMLType xmlType, string userName = "", string companyName = "", string xml = "")
        {
            this.UserName = userName;
            this.CompanyName = companyName;
            this.XML = xml;
            this.XML_Type = xmlType;
        }
    }
}