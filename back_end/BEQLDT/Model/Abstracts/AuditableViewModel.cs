using System;

namespace BEQLDT.Model.Abstracts
{
    public class AuditableViewModel
    {
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedUser { get; set; }
        public int? UpdateUser { get; set; }
        public string Description { get; set; }
    }
}
