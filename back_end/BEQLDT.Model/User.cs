using BEQLDT.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEQLDT.Model
{
    [Table("Users")]
    public class User: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Username { get; set; }
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
        [MaxLength(150)]
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Degree { get; set; }
        public string SSOSub { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<Topic> TopicAll { get; set; }
    }
}
