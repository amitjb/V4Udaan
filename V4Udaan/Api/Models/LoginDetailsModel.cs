using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class LoginDetailsModel
    {
        public string client_id { get; set; }
        public string client_group { get; set; }
        public string client_name { get; set; }
        public string client_username { get; set; }
        public string address { get; set; }
        public string contact_person { get; set; }
        public string contact_no { get; set; }
        public string mail_id { get; set; }
        public string activeTill { get; set; }
        public string client_image { get; set; }
        public string path { get; set; }
    }
}