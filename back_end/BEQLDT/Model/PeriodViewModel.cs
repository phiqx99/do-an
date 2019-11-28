using System;
using BEQLDT.Model.Abstracts;

namespace BEQLDT.Model
{
    public class PeriodViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public string Caption { get; set; }
    }
}
