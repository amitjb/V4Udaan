using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class LoginResquestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginResquestModel(string UserName = "", string Password = "")
        {
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}
