using BEQLDT.Model.Abstract;

namespace BEQLDT.Service.Model
{
    public class GroupUsers : Auditable
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string NameGroup { get; set; }
        
    }
}
