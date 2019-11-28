using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Groups")]
    public class Group: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(256)]
        [Required]
        public string NameGroup { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
