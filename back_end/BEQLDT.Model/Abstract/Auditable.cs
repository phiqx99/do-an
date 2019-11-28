using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEQLDT.Model.Abstract
{
    public class Auditable
    {
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedUser { get; set; }
        public int? UpdateUser { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
