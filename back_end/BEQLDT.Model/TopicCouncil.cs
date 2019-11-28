using BEQLDT.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("TopicCouncils")]
    public class TopicCouncil: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CouncilId { get; set; }
        public int? TopicId { get; set; }
        [ForeignKey("CouncilId")]
        public virtual Council Council { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }

    }
}
