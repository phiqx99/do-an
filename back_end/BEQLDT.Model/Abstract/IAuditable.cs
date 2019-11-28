using System;
using System.Collections.Generic;
using System.Text;

namespace BEQLDT.Model.Abstract
{
    public interface IAuditable
    {
        bool Active { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? UpdateAt { get; set; }
        int? CreatedUser { get; set; }
        int? UpdateUser { get; set; }
        string Description { get; set; }
    }
}
