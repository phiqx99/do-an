using BEQLDT.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Topics")]
    public class Topic: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameTopic { get; set; }
        public int? UserId { get; set; }
        public int? PeriodId { get; set; }
        public int? SchoolId { get; set; }
        public int? DecisionId { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("PeriodId")]
        public virtual Period Period { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("DecisionId")]
        public virtual Decision Decision { get; set; }

        public virtual ICollection<TopicCouncil> TopicCouncil { get; set; }
        public virtual ICollection<Filed> Fileds { get; set; }
    }
}
