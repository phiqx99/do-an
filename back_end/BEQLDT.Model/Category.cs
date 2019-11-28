using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Categorys")]
    public class Category: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(256)]
        [Required]
        public string NameCategory { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
    }
}
