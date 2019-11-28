using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class TopicViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string NameTopic { get; set; }
        public int? PeriodId { get; set; }
        public int? SchoolId { get; set; }
        public int? FileId { get; set; }
        public string NameFile { get; set; }
        public string Base64File { get; set; }
        public int? DecisionId { get; set; }
        public int? CategoryId { get; set; }
    }
}
