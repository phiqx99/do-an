using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BEQLDT.Model.Auditable
{
    public class IAuditableViewModel
    {
        bool Active { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? UpdateAt { get; set; }
        int CreatedUser { get; set; }
        int UpdateUser { get; set; }
        string Description { get; set; }
    }
}
