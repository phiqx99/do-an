using BEQLDT.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Permissions")]
    public class Permission: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
