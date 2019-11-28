using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Files")]
    public class Filed: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NameFile { get; set; }
        public string Base64File { get; set; }
        public int TopicId { get; set; }
        //[ForeignKey("TopicId")]
        //public virtual Topic Topic { get; set; }
    }
}
