using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.DeleteUserByLogin
{
    public class DeleteUserByLoginCommand
    {
        public string Login { get; set; }

        public string RevokedByLogin { get; set; }

        public bool IsSoftDelete { get; set; }

        public DeleteUserByLoginCommand(string login, string revokedByLogin, bool isSoftDelete)
        {
            Login = login;
            RevokedByLogin = revokedByLogin;
            IsSoftDelete = isSoftDelete;
        }
    }
}
