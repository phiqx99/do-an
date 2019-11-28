using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Schools")]
    public class School: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(350)]
        [Required]
        public string NameSchool { get; set; }
        [MaxLength(15)]
        [Required]
        public int Phone { get; set; }
        [MaxLength(256)]
        [Required]
        public string Email { get; set; }
        [MaxLength(256)]
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
    }
}
