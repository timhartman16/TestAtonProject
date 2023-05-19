using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommand
    {
        public string Login { get; set; }
        public string NewLogin { get; set; }
        public string ModifiedBy { get; set; }

        public UpdateUserLoginCommand(string login, string newLogin, string modifiedBy)
        {
            Login = login;
            NewLogin = newLogin;
            ModifiedBy = modifiedBy;
        }
    }
}
