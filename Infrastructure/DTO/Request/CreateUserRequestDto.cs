using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Request
{
    public class CreateUserRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsAdmin { get; set; }
    }
}
