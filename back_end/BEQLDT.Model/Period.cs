using BEQLDT.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Periods")]
    public class Period: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        [MaxLength(256)]
        public string Caption { get; set; }
        public virtual ICollection<Topic> TopicAll { get; set; }

    }
}
