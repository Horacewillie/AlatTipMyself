using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.DTO
{
    public class UserDetailDto
    {
        public string AcctNumber { get; set; }
       
        public string AcctName { get; set; }
    
        public string Email { get; set; }
        public decimal AcctBalance { get; set; }
    }
}
