using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Response
{
    public class UserResponseDto
    {
        public string Name { get; private set; }
        public int Gender { get; private set; }
        public DateTime? Birthday { get; private set; }
        public bool isActive { get; private set; }
    }
}
