using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class RoleViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string NameRole { get; set; }
    }
}
