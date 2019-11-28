using BEQLDT.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("GroupUsers")]
    public class GroupUser: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? CouncilId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("CouncilId")]
        public virtual Council Council { get; set; }
  
    }
}
