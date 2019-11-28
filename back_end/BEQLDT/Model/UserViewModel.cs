using System;
using BEQLDT.Model.Abstracts;

namespace BEQLDT.Model
{
    public class UserViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string SSOSub { get; set; }
        public string Degree { get; set; }
    }
}
