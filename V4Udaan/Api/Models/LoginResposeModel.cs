using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class LoginResposeModel
    {
        public bool status { get; set; }
        public LoginDetailsModel details { get; set; }
    }    
}