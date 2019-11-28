using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class GroupUserViewModel: AuditableViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? CouncilId { get; set; }
    }
}
