using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Roles")]
    public class Role: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(256)]
        public string NameRole { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
