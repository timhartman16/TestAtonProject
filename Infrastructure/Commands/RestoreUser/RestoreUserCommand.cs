using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.RestoreUser
{
    public class RestoreUserCommand
    {
        public string Login { get; set; }
        public string ModifiedBy { get;set; }

        public RestoreUserCommand(string login, string modifiedBy)
        {
            Login = login;
            ModifiedBy = modifiedBy;
        }
    }
}
