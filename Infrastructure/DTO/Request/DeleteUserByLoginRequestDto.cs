using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Request
{
    public class DeleteUserByLoginRequestDto
    {
        public string Login { get; set; }

        public bool IsSoftDelete { get; set; }
    }
}
