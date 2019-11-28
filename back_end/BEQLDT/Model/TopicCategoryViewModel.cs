using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class TopicCategoryViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? TopicId { get; set; }
    }
}
