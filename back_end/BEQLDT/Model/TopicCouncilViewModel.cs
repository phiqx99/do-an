using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class TopicCouncilViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public int? CouncilId { get; set; }
        public int? TopicId { get; set; }
    }
}
