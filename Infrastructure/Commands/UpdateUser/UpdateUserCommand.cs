using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string ModifiedBy { get; set; }

        public UpdateUserCommand(string login, string name, int gender, DateTime? birthday, string modifiedBy)
        {
            Login = login;
            Name = name;
            Gender = gender;
            Birthday = birthday;
            ModifiedBy = modifiedBy;
        }
    }
}
