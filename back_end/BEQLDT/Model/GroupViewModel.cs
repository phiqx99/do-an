using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class GroupViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string NameGroup { get; set; }
    }
}
