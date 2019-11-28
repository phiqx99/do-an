

using BEQLDT.Model.Abstracts;

namespace BEQLDT.Model
{
    public class CouncilViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string NameCouncil { get; set; }
    }
}
