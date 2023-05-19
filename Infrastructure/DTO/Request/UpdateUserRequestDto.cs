using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Request
{
    public class UpdateUserRequestDto
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
