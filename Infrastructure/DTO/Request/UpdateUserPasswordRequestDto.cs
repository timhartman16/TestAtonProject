using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Request
{
    public class UpdateUserPasswordRequestDto
    {
        public string Login { get; set; }
        public string NewPassword { get; set; }
    }
}
