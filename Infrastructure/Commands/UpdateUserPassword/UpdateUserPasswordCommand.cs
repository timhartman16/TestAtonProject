using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommand
    {
        public string Login { get; set; }
        public string NewPassword { get; set; }
        public string ModifiedBy { get; set; }

        public UpdateUserPasswordCommand(string login, string newPassword, string modifiedBy)
        {
            Login = login;
            NewPassword = newPassword;
            ModifiedBy = modifiedBy;
        }
    }
}
