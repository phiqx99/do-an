using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class SchoolViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string NameSchool { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
