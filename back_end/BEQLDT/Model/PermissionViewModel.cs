using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class PermissionViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? RoleId { get; set; }
    }
}
