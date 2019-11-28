using BEQLDT.Model.Abstracts;

namespace BEQLDT.Model
{
    public class FileViewModel: AuditableViewModel
    {
        public int Id { get; set; }
        public string NameFile { get; set; }
        public string Base64File { get; set; }
        public int TopicId { get; set; }
    }
}
