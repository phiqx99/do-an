

using BEQLDT.Model.Abstracts;

namespace BEQLDT.Model
{
    public class DecisionViewModel: AuditableViewModel
    {
        public int Id { get; set; }
        public string NameDecision { get; set; }
    }
}
