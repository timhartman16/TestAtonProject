using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Request
{
    public class UpdateUserLoginRequestDto
    {
        public string Login { get; set; }
        public string NewLogin { get; set; }
    }
}
