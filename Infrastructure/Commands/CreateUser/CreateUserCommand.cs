using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public CreateUserCommand(string loginCreatedBy, string login, string password, string name, int gender, DateTime? birthday, bool isAdmin)
        {
            Login = login;
            Password = password;
            Name = name;
            Gender = gender;
            Birthday = birthday;
            IsAdmin = isAdmin;
            CreatedOn = DateTime.Now;
            CreatedBy = loginCreatedBy;
        }
    }
}
